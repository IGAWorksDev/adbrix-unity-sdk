#import <Foundation/Foundation.h>
#import <objc/runtime.h>
#import <UIKit/UIKit.h>
#import "AdbrixUnitySwizzler.h"
#import <AdbrixSDK/AdbrixSDK.h>
#import "AdbrixUnitySharedInstance.h"

@implementation UIApplication (AdbrixDelegate)

+ (void)load 
{
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        SEL originalSelector = @selector(setDelegate:);
        SEL swizzledSelector = @selector(adbrix_setDelegate:);
        Method originalMethod = class_getInstanceMethod(self, originalSelector);
        Method swizzledMethod = class_getInstanceMethod(self, swizzledSelector);

        method_exchangeImplementations(originalMethod, swizzledMethod);
    });
}

- (void)adbrix_setDelegate:(id<UIApplicationDelegate>)delegate 
{
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        Class appDelegate = [delegate class];
        adbrix_swizzle(self.class,
                      @selector(adbrix_application:didFinishLaunchingWithOptions:),
                      appDelegate,
                      @selector(application:didFinishLaunchingWithOptions:));
        
        adbrix_swizzle(self.class,
                      @selector(adbrix_application:openURL:options:),
                      appDelegate,
                      @selector(application:openURL:options:));
        
        adbrix_swizzle(self.class,
                      @selector(adbrix_application:continueUserActivity:restorationHandler:),
                      appDelegate,
                      @selector(application:continueUserActivity:restorationHandler:));
        
        [self adbrix_setDelegate:delegate];
    });
}

#pragma mark - Method Swizzling

- (BOOL)adbrix_application:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)launchOptions 
{
    NSDictionary *infoDictionary = [[NSBundle mainBundle] infoDictionary];
    if (!infoDictionary) {
        NSLog(@"[AdbrixUnity] Failed to get Info.plist dictionary");
        return YES;
    }
    
    NSString *appKey = [infoDictionary objectForKey:@"com.igaworks.adbrix.appkey"];
    if (!appKey || ![appKey isKindOfClass:[NSString class]] || [appKey length] == 0) {
        NSLog(@"[AdbrixUnity] Invalid or missing appKey in Info.plist");
        return YES;
    }
    
    NSString *secretKey = [infoDictionary objectForKey:@"com.igaworks.adbrix.secretkey"];
    if (!secretKey || ![secretKey isKindOfClass:[NSString class]] || [secretKey length] == 0) {
        NSLog(@"[AdbrixUnity] Invalid or missing secretKey in Info.plist");
        return YES;
    }
    
    NSNumber *logEnabled = [infoDictionary objectForKey:@"com.igaworks.adbrix.logenabled"];
    if (!logEnabled || ![logEnabled isKindOfClass:[NSNumber class]]) {
        logEnabled = @NO;
    }
    
    NSNumber *attTimeout = [infoDictionary objectForKey:@"com.igaworks.adbrix.att.timeout"];
    if (!attTimeout || ![attTimeout isKindOfClass:[NSNumber class]] || [attTimeout intValue] < 0) {
        attTimeout = @0;
    }

    NSNumber *isBlockDeferredDeepLinkLaunch = [infoDictionary objectForKey:@"com.igaworks.adbrix.isBlockDeferredDeepLinkLaunch"];
    if (!isBlockDeferredDeepLinkLaunch || ![isBlockDeferredDeepLinkLaunch isKindOfClass:[NSNumber class]]) {
        isBlockDeferredDeepLinkLaunch = @NO;
    }

    [[Adbrix shared] sdkInitWithAppkey:appKey
                            secretKey:secretKey
                          extraConfig:@{
        @"setLog": logEnabled,
        @"trackingAuthorizeTimeOutRawValue": attTimeout
    }];
    
    if ([isBlockDeferredDeepLinkLaunch boolValue]) {
        [[Adbrix shared] setDeferredDeepLinkDelegate:[AdbrixUnitySharedInstance sharedInstance]];
    }
    
    if ([self respondsToSelector:@selector(adbrix_application:didFinishLaunchingWithOptions:)])
        return [self adbrix_application:application didFinishLaunchingWithOptions:launchOptions];
    return YES;
}

- (BOOL)adbrix_application:(UIApplication *)application openURL:(NSURL *)url options:(NSDictionary<UIApplicationOpenURLOptionsKey,id> *)options 
{
    [[Adbrix shared] deepLinkOpenWithUrl:url];
    
    if ([self respondsToSelector:@selector(adbrix_application:openURL:options:)])
        return [self adbrix_application:application openURL:url options:options];
    return YES;
}

- (BOOL)adbrix_application:(UIApplication *)application continueUserActivity:(NSUserActivity *)userActivity restorationHandler:(void (^)(NSArray<id<UIUserActivityRestoring>> * _Nullable))restorationHandler 
{
    if ([userActivity.activityType isEqualToString:NSUserActivityTypeBrowsingWeb]) {
        NSURL *url = userActivity.webpageURL;
        if (url) {
            [[Adbrix shared] deepLinkOpenWithUrl:url];
        }
    }
    
    if ([self respondsToSelector:@selector(adbrix_application:continueUserActivity:restorationHandler:)])
        return [self adbrix_application:application continueUserActivity:userActivity restorationHandler:restorationHandler];
    return YES;
}

@end
