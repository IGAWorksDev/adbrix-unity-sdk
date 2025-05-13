using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdbrixPlugin {
    public interface IAdbrix {
        void Init(string applicationId, string secretKey);
        void InitWithConfig(string applicationId, string secretKey, Dictionary<string, object> properties = null);
        void LogEvent(string eventName);
        void LogEvent(string eventName, Dictionary<string, object> properties = null);
        void EnableSDK();
        void DisableSDK();
        void BlockDeferredDeepLinkLaunch(AdbrixDeepLinkCallback callback);
        void ATTAuthorized(bool isAuthorized);
    }
}
