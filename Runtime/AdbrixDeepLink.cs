using System;
using UnityEngine;

namespace AdbrixPlugin {
    [Serializable]
    public class AdbrixDeepLink {
        public int result;
        public string deepLink;
        
        public static AdbrixDeepLink CreateFromJSON(string jsonString) {
            return JsonUtility.FromJson<AdbrixDeepLink>(jsonString);
        }
    }
}