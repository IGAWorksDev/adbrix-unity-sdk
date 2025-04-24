#import "AdbrixUnitySharedInstance.h"

extern void UnitySendMessage(const char* gameObjectName, const char* methodName, const char* message);

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
    return self;
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
