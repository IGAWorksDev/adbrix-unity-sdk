#import <Foundation/Foundation.h>
#import <objc/runtime.h>
#import <UIKit/UIKit.h>
#import "AdbrixUnitySwizzler.h"
#import <AdbrixSDK/AdbrixSDK.h>
#import "AdbrixUnitySharedInstance.h"
#import "AdbrixUnitySceneDelegate.h"

// Flag to track if Scene lifecycle is being used
static BOOL _adbrixSceneLifecycleEnabled = NO;

#pragma mark - Scene Delegate Detection Helper

static Class adbrix_findSceneDelegateClass(void) {
    if (@available(iOS 13.0, *)) {
        NSDictionary *infoPlist = [[NSBundle mainBundle] infoDictionary];
        NSDictionary *sceneManifest = infoPlist[@"UIApplicationSceneManifest"];
        
        if (!sceneManifest) return nil;
        
        NSDictionary *sceneConfigurations = sceneManifest[@"UISceneConfigurations"];
        if (!sceneConfigurations) return nil;
        
        NSArray *windowSceneRoles = sceneConfigurations[@"UIWindowSceneSessionRoleApplication"];
        if (!windowSceneRoles || windowSceneRoles.count == 0) return nil;
        
        NSDictionary *defaultConfig = windowSceneRoles.firstObject;
        NSString *delegateClassName = defaultConfig[@"UISceneDelegateClassName"];
        
        if (!delegateClassName || delegateClassName.length == 0) return nil;
        
        // Handle $(PRODUCT_MODULE_NAME).ClassName format
        if ([delegateClassName containsString:@"."]) {
            NSArray *components = [delegateClassName componentsSeparatedByString:@"."];
            delegateClassName = components.lastObject;
        }
        
        Class sceneClass = NSClassFromString(delegateClassName);
        if (sceneClass) {
            NSLog(@"[AdbrixUnity] Found SceneDelegate class: %@", delegateClassName);
            return sceneClass;
        }
        
        // Try common Unity SceneDelegate class names
        NSArray *commonClassNames = @[
            @"UnityScene",
            @"AppUnityScene", 
            @"SceneDelegate"
        ];
        
        for (NSString *className in commonClassNames) {
            sceneClass = NSClassFromString(className);
            if (sceneClass) {
                NSLog(@"[AdbrixUnity] Found SceneDelegate class (fallback): %@", className);
                return sceneClass;
            }
        }
    }
    
    return nil;
}

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
        
        // Always swizzle didFinishLaunchingWithOptions - called in both lifecycles
        adbrix_swizzle(self.class,
                      @selector(adbrix_application:didFinishLaunchingWithOptions:),
                      appDelegate,
                      @selector(application:didFinishLaunchingWithOptions:));
        
        // Detect Scene lifecycle
        Class sceneClass = adbrix_findSceneDelegateClass();
        
        if (sceneClass) {
            // Scene lifecycle detected - swizzle SceneDelegate methods
            _adbrixSceneLifecycleEnabled = YES;
            NSLog(@"[AdbrixUnity] Scene lifecycle detected. Swizzling SceneDelegate methods.");
            [AdbrixUnitySceneDelegate swizzleSceneDelegateClass:sceneClass];
        }
        
        // Always swizzle AppDelegate URL methods as fallback
        // These will be called in legacy lifecycle or if Scene lifecycle doesn't handle them
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
        if ([self respondsToSelector:@selector(adbrix_application:didFinishLaunchingWithOptions:)])
            return [self adbrix_application:application didFinishLaunchingWithOptions:launchOptions];
        return YES;
    }
    
    NSString *appKey = [infoDictionary objectForKey:@"com.igaworks.adbrix.appkey"];
    if (!appKey || ![appKey isKindOfClass:[NSString class]] || [appKey length] == 0) {
        NSLog(@"[AdbrixUnity] Invalid or missing appKey in Info.plist");
        if ([self respondsToSelector:@selector(adbrix_application:didFinishLaunchingWithOptions:)])
            return [self adbrix_application:application didFinishLaunchingWithOptions:launchOptions];
        return YES;
    }
    
    NSString *secretKey = [infoDictionary objectForKey:@"com.igaworks.adbrix.secretkey"];
    if (!secretKey || ![secretKey isKindOfClass:[NSString class]] || [secretKey length] == 0) {
        NSLog(@"[AdbrixUnity] Invalid or missing secretKey in Info.plist");
        if ([self respondsToSelector:@selector(adbrix_application:didFinishLaunchingWithOptions:)])
            return [self adbrix_application:application didFinishLaunchingWithOptions:launchOptions];
        return YES;
    }
    
    NSNumber *logLevel = [infoDictionary objectForKey:@"com.igaworks.adbrix.loglevel"];
    BOOL hasValidLogLevel = NO;
    if ([logLevel isKindOfClass:[NSNumber class]]) {
        NSInteger value = [logLevel integerValue];
        hasValidLogLevel = (value >= 2 && value <= 6);
    }
    
    NSNumber *attTimeout = [infoDictionary objectForKey:@"com.igaworks.adbrix.att.timeout"];
    if (!attTimeout || ![attTimeout isKindOfClass:[NSNumber class]] || [attTimeout intValue] < 0) {
        attTimeout = @0;
    }

    NSNumber *isBlockDeferredDeepLinkLaunch = [infoDictionary objectForKey:@"com.igaworks.adbrix.isBlockDeferredDeepLinkLaunch"];
    if (!isBlockDeferredDeepLinkLaunch || ![isBlockDeferredDeepLinkLaunch isKindOfClass:[NSNumber class]]) {
        isBlockDeferredDeepLinkLaunch = @NO;
    }

    NSMutableDictionary *extraConfig = [@{
        @"trackingAuthorizeTimeOutRawValue": attTimeout
    } mutableCopy];

    if (hasValidLogLevel) {
        extraConfig[@"ABLogLevel"] = logLevel;
    }

    [[Adbrix shared] sdkInitWithAppkey:appKey
                            secretKey:secretKey
                          extraConfig:extraConfig];
    
    if ([isBlockDeferredDeepLinkLaunch boolValue]) {
        [[Adbrix shared] setDeferredDeepLinkDelegate:[AdbrixUnitySharedInstance sharedInstance]];
    }
    
    NSLog(@"[AdbrixUnity] SDK initialized. Scene lifecycle: %@", _adbrixSceneLifecycleEnabled ? @"YES" : @"NO");
    
    if ([self respondsToSelector:@selector(adbrix_application:didFinishLaunchingWithOptions:)])
        return [self adbrix_application:application didFinishLaunchingWithOptions:launchOptions];
    return YES;
}

- (BOOL)adbrix_application:(UIApplication *)application openURL:(NSURL *)url options:(NSDictionary<UIApplicationOpenURLOptionsKey,id> *)options 
{
    // In Scene lifecycle, this method may not be called
    // Deep links are handled by SceneDelegate instead
    if (!_adbrixSceneLifecycleEnabled) {
        [[Adbrix shared] deepLinkOpenWithUrl:url];
    }
    
    if ([self respondsToSelector:@selector(adbrix_application:openURL:options:)])
        return [self adbrix_application:application openURL:url options:options];
    return YES;
}

- (BOOL)adbrix_application:(UIApplication *)application continueUserActivity:(NSUserActivity *)userActivity restorationHandler:(void (^)(NSArray<id<UIUserActivityRestoring>> * _Nullable))restorationHandler 
{
    // In Scene lifecycle, this method may not be called
    // Universal links are handled by SceneDelegate instead
    if (!_adbrixSceneLifecycleEnabled) {
        if ([userActivity.activityType isEqualToString:NSUserActivityTypeBrowsingWeb]) {
            NSURL *url = userActivity.webpageURL;
            if (url) {
                [[Adbrix shared] deepLinkOpenWithUrl:url];
            }
        }
    }
    
    if ([self respondsToSelector:@selector(adbrix_application:continueUserActivity:restorationHandler:)])
        return [self adbrix_application:application continueUserActivity:userActivity restorationHandler:restorationHandler];
    return YES;
}

@end
