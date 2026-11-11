#import "AdbrixUnitySceneDelegate.h"
#import "AdbrixUnitySwizzler.h"
#import <AdbrixSDK/AdbrixSDK.h>
#import <objc/runtime.h>

#pragma mark - SceneDelegate Swizzled Methods Category

@interface NSObject (AdbrixSceneDelegate)

- (void)adbrix_scene:(UIScene *)scene willConnectToSession:(UISceneSession *)session options:(UISceneConnectionOptions *)connectionOptions API_AVAILABLE(ios(13.0));
- (void)adbrix_scene:(UIScene *)scene openURLContexts:(NSSet<UIOpenURLContext *> *)URLContexts API_AVAILABLE(ios(13.0));
- (void)adbrix_scene:(UIScene *)scene continueUserActivity:(NSUserActivity *)userActivity API_AVAILABLE(ios(13.0));

@end

@implementation NSObject (AdbrixSceneDelegate)

- (void)adbrix_scene:(UIScene *)scene willConnectToSession:(UISceneSession *)session options:(UISceneConnectionOptions *)connectionOptions API_AVAILABLE(ios(13.0)) {
    // Handle cold start deep links from connectionOptions
    if (connectionOptions.URLContexts.count > 0) {
        for (UIOpenURLContext *context in connectionOptions.URLContexts) {
            NSURL *url = context.URL;
            if (url) {
                [[Adbrix shared] deepLinkOpenWithUrl:url];
            }
        }
    }
    
    // Handle Universal Links on cold start
    for (NSUserActivity *userActivity in connectionOptions.userActivities) {
        if ([userActivity.activityType isEqualToString:NSUserActivityTypeBrowsingWeb]) {
            NSURL *url = userActivity.webpageURL;
            if (url) {
                [[Adbrix shared] deepLinkOpenWithUrl:url];
            }
        }
    }
    
    // Call original implementation if exists
    if ([self respondsToSelector:@selector(adbrix_scene:willConnectToSession:options:)]) {
        [self adbrix_scene:scene willConnectToSession:session options:connectionOptions];
    }
}

- (void)adbrix_scene:(UIScene *)scene openURLContexts:(NSSet<UIOpenURLContext *> *)URLContexts API_AVAILABLE(ios(13.0)) {
    // Handle scheme deep links when app is in background
    for (UIOpenURLContext *context in URLContexts) {
        NSURL *url = context.URL;
        if (url) {
            [[Adbrix shared] deepLinkOpenWithUrl:url];
        }
    }
    
    // Call original implementation if exists
    if ([self respondsToSelector:@selector(adbrix_scene:openURLContexts:)]) {
        [self adbrix_scene:scene openURLContexts:URLContexts];
    }
}

- (void)adbrix_scene:(UIScene *)scene continueUserActivity:(NSUserActivity *)userActivity API_AVAILABLE(ios(13.0)) {
    // Handle Universal Links when app is in background
    if ([userActivity.activityType isEqualToString:NSUserActivityTypeBrowsingWeb]) {
        NSURL *url = userActivity.webpageURL;
        if (url) {
            [[Adbrix shared] deepLinkOpenWithUrl:url];
        }
    }
    
    // Call original implementation if exists
    if ([self respondsToSelector:@selector(adbrix_scene:continueUserActivity:)]) {
        [self adbrix_scene:scene continueUserActivity:userActivity];
    }
}

@end

#pragma mark - AdbrixUnitySceneDelegate Implementation

@implementation AdbrixUnitySceneDelegate

+ (void)swizzleSceneDelegateClass:(Class)sceneClass {
    if (!sceneClass) return;
    
    if (@available(iOS 13.0, *)) {
        // Swizzle scene:willConnectToSession:options: for cold start deep links
        adbrix_swizzle([NSObject class],
                      @selector(adbrix_scene:willConnectToSession:options:),
                      sceneClass,
                      @selector(scene:willConnectToSession:options:));
        
        // Swizzle scene:openURLContexts: for scheme deep links
        adbrix_swizzle([NSObject class],
                      @selector(adbrix_scene:openURLContexts:),
                      sceneClass,
                      @selector(scene:openURLContexts:));
        
        // Swizzle scene:continueUserActivity: for Universal Links
        adbrix_swizzle([NSObject class],
                      @selector(adbrix_scene:continueUserActivity:),
                      sceneClass,
                      @selector(scene:continueUserActivity:));
    }
}

@end
