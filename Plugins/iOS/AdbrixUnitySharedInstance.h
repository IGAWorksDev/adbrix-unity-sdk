#import <UIKit/UIKit.h>
#import <Foundation/Foundation.h>
#import <AdbrixSDK/AdbrixSDK.h>

@interface AdbrixUnitySharedInstance : NSObject <AdbrixDeferredDeepLinkDelegate>

@property (nonatomic, assign, readonly) BOOL sdkInitialized;

+ (instancetype)sharedInstance;

- (void)sendDeepLinkToUnity:(NSString *)deepLinkData;

- (void)didReceiveWithDeferredDeepLink:(AdbrixDeepLink * _Nonnull)deferredDeepLink;

/**
 * Handles deep link URL with SDK initialization state awareness.
 * If SDK is not initialized yet, the URL is queued and processed after initialization.
 * @param url The deep link URL to process
 */
- (void)handleDeepLinkURL:(NSURL *)url;

/**
 * Marks SDK as initialized and flushes any pending deep link URLs.
 * Called from didFinishLaunchingWithOptions after sdkInitWithAppkey completes.
 */
- (void)markSDKInitializedAndFlushPendingDeepLinks;

@end 