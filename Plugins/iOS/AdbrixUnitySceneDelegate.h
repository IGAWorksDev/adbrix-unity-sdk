#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>

NS_ASSUME_NONNULL_BEGIN

/**
 * SceneDelegate swizzler for iOS 13+ Scene lifecycle support.
 * Handles deep link events when app uses UIScene-based lifecycle (required for Xcode 27+/iOS 27+).
 */
@interface AdbrixUnitySceneDelegate : NSObject

/**
 * Sets up method swizzling for SceneDelegate methods.
 * Called automatically when the SceneDelegate class is detected.
 */
+ (void)swizzleSceneDelegateClass:(Class)sceneClass;

@end

NS_ASSUME_NONNULL_END
