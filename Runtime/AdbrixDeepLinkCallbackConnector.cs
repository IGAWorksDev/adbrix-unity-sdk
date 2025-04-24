using UnityEngine;

namespace AdbrixPlugin {
#if UNITY_ANDROID
public class AdbrixDeepLinkCallbackConnector : AndroidJavaProxy {
    private AdbrixDeepLinkCallback callback;
    public const string ANDROID_UNITY_CALLBACK_CLASS_NAME = "com.igaworks.adbrix.unity.AdbrixUnityCallback";

    public AdbrixDeepLinkCallbackConnector(AdbrixDeepLinkCallback callback) : base(ANDROID_UNITY_CALLBACK_CLASS_NAME) {
        this.callback = callback;
    }
    void onCallback(string result) {
        if (callback != null) {
            if (result != null && result.Length > 0) {
                AdbrixDeepLink adbrixDeepLink = AdbrixDeepLink.CreateFromJSON(result);
                callback.OnAdbrixDeepLink(adbrixDeepLink);
            } else {
                callback.OnAdbrixDeepLink(null);
            }
        }
    }
}
#elif UNITY_IOS

public class AdbrixDeepLinkCallbackConnector : MonoBehaviour {
    private static AdbrixDeepLinkCallbackConnector _instance;
    private static AdbrixDeepLinkCallback _callback;
    
    public static AdbrixDeepLinkCallbackConnector Instance {
        get { return _instance; }
    }
    
    public void SetCallback(AdbrixDeepLinkCallback callback) {
        _callback = callback;
    }
    
    public void HandleDeepLinkFromiOS(string deepLinkJson) {
        if (string.IsNullOrEmpty(deepLinkJson)) return;
        
        try {
            Debug.Log($"[Adbrix] Received deeplink: {deepLinkJson}");
            
            AdbrixDeepLink deepLink = AdbrixDeepLink.CreateFromJSON(deepLinkJson);
            
            if (_callback != null) {
                _callback.OnAdbrixDeepLink(deepLink);
            } else {
                Debug.LogWarning("[Adbrix] Received deeplink but no callback is registered.");
            }
        }
        catch (System.Exception e) {
            Debug.LogError($"[Adbrix] Error handling deeplink: {e.Message}\n{e.StackTrace}");
        }
    }
    
    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(gameObject);
            return;
        }
        
        _instance = this;
        DontDestroyOnLoad(gameObject);
        
        Debug.Log("[Adbrix] DeepLinkCallbackConnector initialized");
    }
    
    private void OnDestroy() {
        if (_instance == this) {
            _instance = null;
        }
    }
}
#endif
}