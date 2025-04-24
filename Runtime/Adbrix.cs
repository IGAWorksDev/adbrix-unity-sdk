#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
#define HAS_ADBRIX_SDK
#endif

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdbrixPlugin
{
    public class Adbrix
    {
        private Adbrix() { }
        private static class SingletonHolder
        {
            public static readonly IAdbrix Instance = CreateBinding();
            private static IAdbrix CreateBinding()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            return new AdbrixAndroid();
#elif UNITY_IOS && !UNITY_EDITOR
            return new AdbrixiOS();
#else
            return new AdbrixNotImplemented();
#endif
            }
        }
        
        public static IAdbrix Binding
        {
            get
            {
                return SingletonHolder.Instance;
            }
        }
        
        public static void Init(string applicationId, string secretKey)
        {
#if HAS_ADBRIX_SDK
            Binding.Init(applicationId, secretKey);
#endif
        }
        public static void InitWithConfig(string applicationId, string secretKey, Dictionary<string, object> config = null)
        {
#if HAS_ADBRIX_SDK
            Binding.InitWithConfig(applicationId, secretKey, config);
#endif
        }
        public static void LogEvent(string eventName)
        {
#if HAS_ADBRIX_SDK
            Binding.LogEvent(eventName);
#endif
        }
        public static void LogEvent(string eventName, Dictionary<string, object> properties = null)
        {
#if HAS_ADBRIX_SDK
            if(properties != null){
                Binding.LogEvent(eventName, properties);
            } else{
                Binding.LogEvent(eventName, null);
            }
#endif
        }
        public static void EnableSDK()
        {
#if HAS_ADBRIX_SDK
            Binding.EnableSDK();
#endif
        }
        public static void DisableSDK()
        {
#if HAS_ADBRIX_SDK
            Binding.DisableSDK();
#endif
        }
        public static void BlockDeferredDeepLinkLaunch(AdbrixDeepLinkCallback callback)
        {
#if HAS_ADBRIX_SDK
            Binding.BlockDeferredDeepLinkLaunch(callback);
#endif
        }
    }
}