using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdbrixPlugin {
public class AdbrixNotImplemented : IAdbrix
{
    public AdbrixNotImplemented()
    {
        Debug.Log("[Adbrix] Using dummy implementation. SDK functionality will not be available.");
    }

    public void Init(string applicationId, string secretKey)
    {
        Debug.Log($"[Adbrix] Dummy Init called with appId: {applicationId}");
    }

    public void InitWithConfig(string applicationId, string secretKey, Dictionary<string, object> properties = null)
    {
        Debug.Log($"[Adbrix] Dummy InitWithConfig called with appId: {applicationId}");
    }

    public void LogEvent(string eventName)
    {
        Debug.Log($"[Adbrix] Dummy LogEvent called: {eventName}");
    }

    public void LogEvent(string eventName, Dictionary<string, object> properties = null)
    {
        Debug.Log($"[Adbrix] Dummy LogEvent called: {eventName} with properties");
    }

    public void EnableSDK()
    {
        Debug.Log("[Adbrix] Dummy EnableSDK called");
    }

    public void DisableSDK()
    {
        Debug.Log("[Adbrix] Dummy DisableSDK called");
    }

    public void BlockDeferredDeepLinkLaunch(AdbrixDeepLinkCallback callback)
    {
        Debug.Log("[Adbrix] Dummy BlockDeferredDeepLinkLaunch called");
    }

    public void ATTAuthrized(bool isAuthorized)
    {
        Debug.Log("[Adbrix] Dummy ATTAuthrized called");
    }
}
}