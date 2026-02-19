#import <AdbrixSDK/AdbrixSDK.h>
#import "AdbrixUnitySharedInstance.h"

extern void UnitySendMessage(const char* gameObjectName, const char* methodName, const char* message);

extern "C" {

void _adbrixInit(const char* appKey, const char* secretKey) {
    [[Adbrix shared] sdkInitWithAppkey:[NSString stringWithUTF8String:appKey]
                              secretKey:[NSString stringWithUTF8String:secretKey]];
}

void _adbrixInitWithConfig(const char* appKey, const char* secretKey, const char* properties) {
    NSDictionary *config = [NSJSONSerialization JSONObjectWithData:[[NSString stringWithUTF8String:properties] dataUsingEncoding:NSUTF8StringEncoding]
                                                           options:0
                                                             error:nil];
    NSDictionary *normalizedConfig = config;
    
    if ([config isKindOfClass:[NSDictionary class]]) {
        NSMutableDictionary *mutableConfig = [config mutableCopy];
        if (mutableConfig[@"ABLogLevel"] != nil) {
            [mutableConfig removeObjectForKey:@"setLog"];
        }
        normalizedConfig = [mutableConfig copy];
    }
    
    [[Adbrix shared] sdkInitWithAppkey:[NSString stringWithUTF8String:appKey]
                              secretKey:[NSString stringWithUTF8String:secretKey]
                           extraConfig:normalizedConfig];
}

void _adbrixLogEvent(const char* eventName) {
    [[Adbrix shared] logEvent:[NSString stringWithUTF8String:eventName]];
}

void _adbrixLogEventWithProps(const char* eventName, const char* properties) {
    NSDictionary *props = [NSJSONSerialization JSONObjectWithData:[[NSString stringWithUTF8String:properties] dataUsingEncoding:NSUTF8StringEncoding]
                                                          options:0
                                                            error:nil];
    [[Adbrix shared] logEvent:[NSString stringWithUTF8String:eventName]
               withProperties:props];
}

void _adbrixEnableSDK(void) {
    [[Adbrix shared] enableSDK];
}

void _adbrixDisableSDK(void) {
    [[Adbrix shared] disableSDK];
}

void _adbrixBlockDeferredDeepLinkLaunch(void) {
    
}

void _adbrixATTAuthorized(bool isAuthorized) {
    [[Adbrix shared] attAuthorized:isAuthorized];
}

}
