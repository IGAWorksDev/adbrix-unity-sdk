using UnityEngine;

public enum AdbrixLogLevel
{
    Verbose = 2,
    Debug = 3,
    Info = 4,
    Warn = 5,
    Error = 6
}


public enum TrackingAuthorizeTimeoutLevel
{
    Off = 0,
    _60 = 60,
    _120 = 120,
    _180 = 180
}

public class AdbrixUnitySettings : ScriptableObject
{
    [Header("Basic Settings")]
    [Tooltip("Enter your Adbrix App Key")]
    public string appKey;
    
    [Tooltip("Enter your Adbrix Secret Key")]
    public string secretKey;

    [Tooltip("Choose whether to prevent navigation to the destination using a deferred deep link")]
     public bool isBlockDeferredDeepLinkLaunch;
    
    [Header("Android Settings")]
    [Tooltip("Enable to collect Google Advertising ID")]
    public bool collectGoogleAdvertisingId;

    [Header("iOS Settings")]
    [Tooltip("iOS ATT permission request timeout (seconds)")]
    [SerializeField]
    private TrackingAuthorizeTimeoutLevel trackingAuthorizeTimeoutLevel = TrackingAuthorizeTimeoutLevel.Off;

    public int TrackingAuthorizeTimeout
    {
        get => (int)trackingAuthorizeTimeoutLevel;
        set
        {
            if (value <= 0) trackingAuthorizeTimeoutLevel = TrackingAuthorizeTimeoutLevel.Off;
            else if (value <= 60) trackingAuthorizeTimeoutLevel = TrackingAuthorizeTimeoutLevel._60;
            else if (value <= 120) trackingAuthorizeTimeoutLevel = TrackingAuthorizeTimeoutLevel._120;
            else trackingAuthorizeTimeoutLevel = TrackingAuthorizeTimeoutLevel._180;
        }
    }

    [Header("Log Settings")]
    [Header("Android Log Settings")]
    [Tooltip("Enable Android logging")]
    public bool androidLogEnabled;
    
    [Tooltip("Set the Android log output level")]
    [SerializeField]
    private AdbrixLogLevel androidLogLevel = AdbrixLogLevel.Verbose;
    
    public AdbrixLogLevel AndroidLogLevel
    {
        get => androidLogLevel;
        set => androidLogLevel = value;
    }

    [Header("iOS Log Settings")]
    [Tooltip("Enable iOS logging")]
    public bool iosLogEnabled;
}
