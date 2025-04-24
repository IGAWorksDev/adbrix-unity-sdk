#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;

public static class AdbrixUnitySettingsCreator
{
    private const string AssetName = "AdbrixUnitySettings";
    private const string ResourcesPath = "Assets/Adbrix/Resources";
    private const string FullPath = ResourcesPath + "/" + AssetName + ".asset";

    [MenuItem("Assets/Adbrix/Settings")]
    public static void CreateSettingsAsset()
    {
        if (!Directory.Exists(ResourcesPath))
        {
            Directory.CreateDirectory(ResourcesPath);
            AssetDatabase.Refresh();
        }

        var existingAsset = AssetDatabase.LoadAssetAtPath<AdbrixUnitySettings>(FullPath);
        if (existingAsset != null)
        {
            Selection.activeObject = existingAsset;
            return;
        }

        var settings = ScriptableObject.CreateInstance<AdbrixUnitySettings>();
        AssetDatabase.CreateAsset(settings, FullPath);
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = settings;

    }
}
#endif
