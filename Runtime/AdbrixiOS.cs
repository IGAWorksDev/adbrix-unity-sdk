#if UNITY_IOS
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace AdbrixPlugin {
public class AdbrixiOS : IAdbrix {
    [DllImport("__Internal")]
    private static extern void _adbrixInit(string appKey, string secretKey);

    [DllImport("__Internal")]
    private static extern void _adbrixInitWithConfig(string appKey, string secretKey, string properties);

    [DllImport("__Internal")]
    private static extern void _adbrixLogEvent(string eventName);

    [DllImport("__Internal")]
    private static extern void _adbrixLogEventWithProps(string eventName, string properties);

    [DllImport("__Internal")]
    private static extern void _adbrixEnableSDK();

    [DllImport("__Internal")]
    private static extern void _adbrixDisableSDK();

    [DllImport("__Internal")]
    private static extern void _adbrixBlockDeferredDeepLinkLaunch();

    [DllImport("__Internal")]
    private static extern void _adbrixATTAuthrized(bool isAuthorized);

    private static readonly object _deeplinkLock = new object();
    private static AdbrixDeepLinkCallback _deeplinkCallback;

    public void Init(string applicationId, string secretKey) {
        _adbrixInit(applicationId, secretKey);
    }

    public void InitWithConfig(string applicationId, string secretKey, Dictionary<string, object> properties = null) {
        string propsJson = properties != null ? Json.Serialize(properties) : "{}";
        _adbrixInitWithConfig(applicationId, secretKey, propsJson);
    }

    public void LogEvent(string eventName) {
        _adbrixLogEvent(eventName);
    }

    public void LogEvent(string eventName, Dictionary<string, object> properties = null) {
        string propsJson = properties != null ? Json.Serialize(properties) : "{}";
        _adbrixLogEventWithProps(eventName, propsJson);
    }

    public void EnableSDK() {
        _adbrixEnableSDK();
    }

    public void DisableSDK() {
        _adbrixDisableSDK();
    }

    public void BlockDeferredDeepLinkLaunch(AdbrixDeepLinkCallback callback) {
        lock (_deeplinkLock) {
            _deeplinkCallback = callback;
        }
        
        EnsureCallbackHandlerExists();
        
        _adbrixBlockDeferredDeepLinkLaunch();
    }

    private void EnsureCallbackHandlerExists() {
        if (Application.isPlaying) {
            if (System.Threading.Thread.CurrentThread.ManagedThreadId == 1) {
                CreateCallbackHandlerIfNeeded();
            }
            else {
                AdbrixThreadDispatcher.Instance().Enqueue(() => {
                    CreateCallbackHandlerIfNeeded();
                });
            }
        }
    }

    private void CreateCallbackHandlerIfNeeded() {
        if (AdbrixDeepLinkCallbackConnector.Instance != null) {
            AdbrixDeepLinkCallbackConnector.Instance.SetCallback(_deeplinkCallback);
            return;
        }
        
        GameObject go = new GameObject("AdbrixDeepLinkCallbackConnector");
        AdbrixDeepLinkCallbackConnector connector = go.AddComponent<AdbrixDeepLinkCallbackConnector>();
        connector.SetCallback(_deeplinkCallback);
        UnityEngine.Object.DontDestroyOnLoad(go);
    }

    public void HandleDeepLink(string deepLinkData) {
        AdbrixDeepLinkCallback callback;
        lock (_deeplinkLock) {
            callback = _deeplinkCallback;
        }
        if (callback != null) {
            AdbrixDeepLink deepLink = AdbrixDeepLink.CreateFromJSON(deepLinkData);
            callback.OnAdbrixDeepLink(deepLink);
        }
    }

    public void ATTAuthrized(bool isAuthorized) {
        _adbrixATTAuthrized(isAuthorized);
    }
}
}
#endif  