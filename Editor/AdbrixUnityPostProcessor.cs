using System;
using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEditor.iOS.Xcode.Extensions;
using UnityEngine;
using UnityEditor.Android;
using System.Xml;
using System.Text.RegularExpressions;

namespace AdbrixPlugin.Editor
{
    public class AdbrixUnityPostProcessor : IPostGenerateGradleAndroidProject
    {
        public int callbackOrder => 0;

        // Android 빌드 후처리
        public void OnPostGenerateGradleAndroidProject(string path)
        {
            try
            {
                var settings = Resources.Load<AdbrixUnitySettings>("AdbrixUnitySettings");
                if (settings == null || string.IsNullOrEmpty(settings.appKey))
                {
                    Debug.LogWarning("[AdbrixUnity] Appkey not set. Skipping meta-data injection for Android.");
                    return;
                }

                string manifestPath = Path.Combine(path, "src/main/AndroidManifest.xml");
                if (!File.Exists(manifestPath))
                {
                    Debug.LogError("[AdbrixUnity] AndroidManifest.xml not found at: " + manifestPath);
                    return;
                }

                var doc = new XmlDocument();
                doc.Load(manifestPath);

                var ns = "http://schemas.android.com/apk/res/android";
                var nsManager = new XmlNamespaceManager(doc.NameTable);
                nsManager.AddNamespace("android", ns);

                var applicationNode = doc.SelectSingleNode("/manifest/application");
                if (applicationNode == null)
                {
                    Debug.LogError("[AdbrixUnity] Couldn't find <application> node in AndroidManifest.");
                    return;
                }

                AddOrUpdateMetaData(doc, applicationNode, ns, "com.igaworks.adbrix.unity.applicationKey", settings.appKey);                

                AddOrUpdateMetaData(doc, applicationNode, ns, "com.igaworks.adbrix.unity.secretKey", settings.secretKey);                

                if (settings.isBlockDeferredDeepLinkLaunch)
                 {
                     AddOrUpdateMetaData(doc, applicationNode, ns, "com.igaworks.adbrix.unity.isBlockDeferredDeepLinkLaunch", "true");
                 }
                 else
                 {
                     AddOrUpdateMetaData(doc, applicationNode, ns, "com.igaworks.adbrix.unity.isBlockDeferredDeepLinkLaunch", "false");
                 }

                if (settings.collectGoogleAdvertisingId)
                {
                    AddOrUpdateMetaData(doc, applicationNode, ns, "com.igaworks.adbrix.unity.collectGoogleAdvertisingId", "true");                }
                else
                {
                    AddOrUpdateMetaData(doc, applicationNode, ns, "com.igaworks.adbrix.unity.collectGoogleAdvertisingId", "false");
                }
                
                AddOrUpdateMetaData(doc, applicationNode, ns, "com.igaworks.adbrix.unity.androidLogLevel", ((int)settings.AndroidLogLevel).ToString());
                 AddOrUpdateMetaData(doc, applicationNode, ns, "com.igaworks.adbrix.unity.androidLogEnable", settings.androidLogEnabled.ToString());                

                doc.Save(manifestPath);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"[AdbrixUnity] Error processing AndroidManifest.xml: {e.Message}");
            }
        }
        
        private void AddOrUpdateMetaData(XmlDocument doc, XmlNode parent, string ns, string name, string value)
        {
            var nsManager = new XmlNamespaceManager(doc.NameTable);
            nsManager.AddNamespace("android", ns);
            
            var existingNode = parent.SelectSingleNode(
                $".//meta-data[@android:name='{name}']",
                nsManager
            );

            if (existingNode != null)
            {
                if (existingNode.Attributes[$"value", ns]?.Value != value)
                {
                    existingNode.Attributes[$"value", ns].Value = value;
                    Debug.Log($"[AdbrixUnity] Updated existing {name} to {value}");
                }
            }
            else
            {
                var meta = doc.CreateElement("meta-data");
                meta.SetAttribute("name", ns, name);
                meta.SetAttribute("value", ns, value);
                parent.AppendChild(meta);
                Debug.Log($"[AdbrixUnity] Added new {name}: {value}");
            }
        }
        
        [PostProcessBuild(999)] 
        public static void OnPostprocessBuild(BuildTarget buildTarget, string path)
        {
            if (buildTarget != BuildTarget.iOS)
                return;
                
            var settings = Resources.Load<AdbrixUnitySettings>("AdbrixUnitySettings");
            if (settings == null || string.IsNullOrEmpty(settings.appKey))
            {
                Debug.LogWarning("[AdbrixUnity] Appkey not set. Skipping info.plist injection for iOS.");
                return;
            }
            
            try
            {
                // info.plist 파일 경로
                string plistPath = Path.Combine(path, "Info.plist");
                
                // plist 파일 읽기
                PlistDocument plist = new PlistDocument();
                plist.ReadFromFile(plistPath);
                
                // appkey 추가 또는 업데이트
                plist.root.SetString("com.igaworks.adbrix.appkey", settings.appKey);
                Debug.Log($"[AdbrixUnity] Added/Updated appkey in iOS Info.plist: {settings.appKey}");
                
                // secretKey 추가
                plist.root.SetString("com.igaworks.adbrix.secretkey", settings.secretKey);
                Debug.Log($"[AdbrixUnity] Added/Updated secretKey in iOS Info.plist");
                
                // ATT 권한 요청 타임아웃 설정
                if (settings.TrackingAuthorizeTimeout > 0)
                {
                    plist.root.SetInteger("com.igaworks.adbrix.att.timeout", settings.TrackingAuthorizeTimeout);
                    Debug.Log($"[AdbrixUnity] Added ATT timeout setting: {settings.TrackingAuthorizeTimeout} seconds");
                }

                plist.root.SetBoolean("com.igaworks.adbrix.isBlockDeferredDeepLinkLaunch", settings.isBlockDeferredDeepLinkLaunch);
                
                // iOS 로그 설정
                plist.root.SetBoolean("com.igaworks.adbrix.logenabled", settings.iosLogEnabled);
                Debug.Log($"[AdbrixUnity] Added iOS log enabled setting: {settings.iosLogEnabled}");
                
                // 변경사항 저장
                plist.WriteToFile(plistPath);
                
            }
            catch (System.Exception e)
            {
                Debug.LogError($"[AdbrixUnity] Error processing iOS build: {e.Message}\n{e.StackTrace}");
            }
        }
    }
}
