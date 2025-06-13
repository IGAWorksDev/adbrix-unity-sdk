using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdbrixPlugin {
#if UNITY_ANDROID
public class AdbrixAndroid : IAdbrix {
    private string Adbrix = "Adbrix";
    // private string AdbrixProperties = "AdbrixProperties";

    private static AndroidJavaObject adbrixUnityActivity;
    private static AndroidJavaObject adbrixUnityBridge;
    public AndroidJavaObject AdbrixUnityActivity {
        get {
            if (adbrixUnityActivity == null) {
                using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
                    adbrixUnityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                }
            }
            return adbrixUnityActivity;
        }
    }
    public AndroidJavaObject AdbrixUnityBridge {
        get {
            if (adbrixUnityBridge != null) {
                return adbrixUnityBridge;
            }
            AndroidJavaClass adbrixBridgeClass = new AndroidJavaClass("com.igaworks.adbrix.unity.AdbrixUnityBridge");
            AndroidJavaObject adbrixBridgeInstance = adbrixBridgeClass.CallStatic<AndroidJavaObject>("getInstance");
            adbrixUnityBridge = adbrixBridgeInstance;
            return adbrixUnityBridge;
        }
    }
    public void Init(string applicationId, string secretKey) {
        AdbrixUnityBridge.Call("init", AdbrixUnityActivity, applicationId, secretKey);
        AdbrixUnityBridge.Call("registerActivityLifecycleCallbacks", AdbrixUnityActivity);
    }
    public void InitWithConfig(string applicationId, string secretKey, Dictionary<string, object> config = null) {
        AdbrixUnityBridge.Call("init", AdbrixUnityActivity, applicationId, secretKey, Json.Serialize(config));
        AdbrixUnityBridge.Call("registerActivityLifecycleCallbacks", AdbrixUnityActivity);
    }

    public void LogEvent(string eventName) {
        var methodParam = new Dictionary<string, object>
        {
                { Constants.KEY_ANY_METHOD_ARGS_1, eventName }
            };
        var message = new Dictionary<string, object>
        {
                { Constants.KEY_STRING_CLASS_NAME, Adbrix },
                { Constants.KEY_STRING_METHOD_NAME, "logEvent" },
                { Constants.KEY_OBJECT_METHOD_PARAM, methodParam }
        };
        AdbrixUnityBridge.Call<string>("invokeWithEncoding", Json.Serialize(message));
    }

    public void LogEvent(string eventName, Dictionary<string, object> properties = null) {
        var methodParam = new Dictionary<string, object>
        {
                { Constants.KEY_ANY_METHOD_ARGS_1, eventName }
            };
        if (properties != null) {
            methodParam[Constants.KEY_ANY_METHOD_ARGS_2] = properties;
        }
        var message = new Dictionary<string, object>
        {
                { Constants.KEY_STRING_CLASS_NAME, Adbrix },
                { Constants.KEY_STRING_METHOD_NAME, "logEvent" },
                { Constants.KEY_OBJECT_METHOD_PARAM, methodParam }
            };
        AdbrixUnityBridge.Call<string>("invokeWithEncoding", Json.Serialize(message));
    }

    public void EnableSDK() {
        var message = new Dictionary<string, object>
        {
                { Constants.KEY_STRING_CLASS_NAME, Adbrix},
                { Constants.KEY_STRING_METHOD_NAME, "enableSDK" }
            };
        AdbrixUnityBridge.Call<string>("invokeWithEncoding", Json.Serialize(message));
    }
    public void DisableSDK() {
        var message = new Dictionary<string, object>
        {
                { Constants.KEY_STRING_CLASS_NAME, Adbrix},
                { Constants.KEY_STRING_METHOD_NAME, "disableSDK" }
            };
        AdbrixUnityBridge.Call<string>("invokeWithEncoding", Json.Serialize(message));
    }
    public void BlockDeferredDeepLinkLaunch(AdbrixDeepLinkCallback callback) {
        AdbrixUnityBridge.Call("blockDeferredDeepLinkLaunch", new AdbrixDeepLinkCallbackConnector(callback));
    }
    public void ATTAuthorized(bool isAuthorized) {}
}
#endif
}