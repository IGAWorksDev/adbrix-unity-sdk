#import "AdbrixUnitySharedInstance.h"
#import <AdbrixSDK/AdbrixSDK.h>

extern void UnitySendMessage(const char* gameObjectName, const char* methodName, const char* message);

@interface AdbrixUnitySharedInstance ()

@property (nonatomic, assign, readwrite) BOOL sdkInitialized;
@property (nonatomic, strong) NSMutableArray<NSURL *> *pendingDeepLinkURLs;

@end

@implementation AdbrixUnitySharedInstance


static AdbrixUnitySharedInstance *_sharedInstance = nil;
static dispatch_once_t onceToken;

+ (instancetype)sharedInstance {
    dispatch_once(&onceToken, ^{
        _sharedInstance = [[self alloc] init];
    });
    return _sharedInstance;
}

+ (instancetype)alloc {
    @synchronized(self) {
        if (_sharedInstance == nil) {
            _sharedInstance = [super alloc];
            return _sharedInstance;
        }
    }
    return _sharedInstance;
}

- (id)copyWithZone:(NSZone *)zone {
    return self;
}

- (instancetype)init {
    self = [super init];
    if (self) {
        _sdkInitialized = NO;
        _pendingDeepLinkURLs = [NSMutableArray array];
    }
    return self;
}

- (void)handleDeepLinkURL:(NSURL *)url {
    if (!url) return;
    
    @synchronized(self) {
        NSLog(@"[AdbrixUnity] handleDeepLinkURL called. sdkInitialized=%@, pendingCount=%lu, url=%@", 
              self.sdkInitialized ? @"YES" : @"NO", 
              (unsigned long)self.pendingDeepLinkURLs.count,
              url.absoluteString);
        
        if (self.sdkInitialized) {
            [[Adbrix shared] deepLinkOpenWithUrl:url];
            NSLog(@"[AdbrixUnity] Deep link processed immediately: %@", url.absoluteString);
        } else {
            [self.pendingDeepLinkURLs addObject:url];
            NSLog(@"[AdbrixUnity] Deep link queued (SDK not initialized): %@", url.absoluteString);
        }
    }
}

- (void)markSDKInitializedAndFlushPendingDeepLinks {
    @synchronized(self) {
        self.sdkInitialized = YES;
        
        if (self.pendingDeepLinkURLs.count > 0) {
            NSLog(@"[AdbrixUnity] Flushing %lu pending deep link(s)", (unsigned long)self.pendingDeepLinkURLs.count);
            
            for (NSURL *url in self.pendingDeepLinkURLs) {
                [[Adbrix shared] deepLinkOpenWithUrl:url];
                NSLog(@"[AdbrixUnity] Flushed pending deep link: %@", url.absoluteString);
            }
            
            [self.pendingDeepLinkURLs removeAllObjects];
        }
    }
}

- (void)sendDeepLinkToUnity:(NSString *)deepLinkData {
    if ([NSThread isMainThread]) {
        UnitySendMessage("AdbrixDeepLinkCallbackConnector", "HandleDeepLinkFromiOS", [deepLinkData UTF8String]);
    } else {
            dispatch_async(dispatch_get_main_queue(), ^{
                UnitySendMessage("AdbrixDeepLinkCallbackConnector", "HandleDeepLinkFromiOS", [deepLinkData UTF8String]);
        });
    }
}

- (void)didReceiveWithDeferredDeepLink:(AdbrixDeepLink * _Nonnull)deferredDeepLink {
    if (!deferredDeepLink) {
        NSLog(@"[Adbrix] Received nil deferred deeplink");
        return;
    }
    
    NSMutableDictionary *deepLinkDict = [NSMutableDictionary dictionary];
        
    if (deferredDeepLink.deepLink) {
        [deepLinkDict setObject: deferredDeepLink.deepLink forKey:@"deepLink"];
    }
    
    
    if (deferredDeepLink.result) {
        
        NSInteger result = deferredDeepLink.result;
        [deepLinkDict setObject:@(result) forKey:@"result"];
    }
    
    NSError *error;
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:deepLinkDict
                                                     options:0
                                                       error:&error];
    
    if (error) {
        NSLog(@"[Adbrix] Error serializing deeplink: %@", error);
        return;
    }
    
    NSString *deepLinkJson = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
    
    [self sendDeepLinkToUnity:deepLinkJson];
}

@end 
