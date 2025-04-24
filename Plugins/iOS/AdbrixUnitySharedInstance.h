#import <UIKit/UIKit.h>
#import <Foundation/Foundation.h>
#import <AdbrixSDK/AdbrixSDK.h>

@interface AdbrixUnitySharedInstance : NSObject <AdbrixDeferredDeepLinkDelegate>

+ (instancetype)sharedInstance;

- (void)sendDeepLinkToUnity:(NSString *)deepLinkData;

- (void)didReceiveWithDeferredDeepLink:(AdbrixDeepLink * _Nonnull)deferredDeepLink;

@end 