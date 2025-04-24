using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace AdbrixPlugin.Editor
{
    [InitializeOnLoad]
    public static class EDMChecker
    {
        public static bool IsEDM4UPresent { get; private set; }

        static EDMChecker()
        {
            IsEDM4UPresent = TypeExists("GooglePlayServices.PlayServicesResolver")
                          || TypeExists("Google.JarResolver.PlayServicesSupport");
        }

        private static bool TypeExists(string fullTypeName)
        {
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(asm => asm.GetTypes())
                .Any(type => type.FullName == fullTypeName);
        }
    }
}
