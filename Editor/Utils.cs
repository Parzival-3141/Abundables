using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Build.Utilities;
using UnityEngine;

namespace Abundables
{
    public static class Utils
    {
        public static bool IsPathValidForEntry(string path)
        {
            string isEditorFolder = $"{Path.DirectorySeparatorChar}Editor";
            string insideEditorFolder = $"{Path.DirectorySeparatorChar}Editor{Path.DirectorySeparatorChar}";
            HashSet<string> excludedExtensions = new(new string[] { ".cs", ".js", ".boo", ".exe", ".dll", ".meta", ".preset", ".asmdef" });

            if (string.IsNullOrEmpty(path))
            {
                return false;
            }

            path = path.Replace('\\', Path.DirectorySeparatorChar).Replace('/', Path.DirectorySeparatorChar);

            if (!path.StartsWith("Assets", StringComparison.Ordinal) && !IsPathValidPackageAsset(path))
                return false;

            string ext = Path.GetExtension(path);
            if (string.IsNullOrEmpty(ext))
            {
                // is folder
                if (path == "Assets")
                    return false;

                if (path.EndsWith(isEditorFolder, StringComparison.Ordinal) || path.Contains(insideEditorFolder))
                    return false;

                if (path == CommonStrings.UnityEditorResourcePath ||
                    path == CommonStrings.UnityDefaultResourcePath ||
                    path == CommonStrings.UnityBuiltInExtraPath)
                    return false;
            }
            else
            {
                // asset type
                if (path.Contains(insideEditorFolder))
                    return false;
                if (excludedExtensions.Contains(ext))
                    return false;
            }

            return true;
        }

        public static bool IsPathValidPackageAsset(string path)
        {
            string[] splitPath = path.ToLower().Split(Path.DirectorySeparatorChar);

            if (splitPath.Length < 3)
                return false;
            if (splitPath[0] != "packages")
                return false;
            if (splitPath[2] == "package.json")
                return false;
            return true;
        }
    }
}