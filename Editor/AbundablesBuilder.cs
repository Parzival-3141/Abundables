using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Abundables
{
    public static class AbundablesBuilder
    {
        public static readonly string bundleCachePath = Path.Combine(Application.dataPath, "../AbundablesBuildCache/");

        public static void BuildBundle(Bundle bundle)
        {
            BuildBundles(new Bundle[] { bundle });
        }

        public static void BuildBundles(Bundle[] bundles)
        {
            if (bundles == null || bundles.Length == 0)
            {
                EditorUtility.DisplayDialog("Abundables Exporter", "Error: Invalid Bundles!", "OK");
                return;
            }

            string savePath = EditorUtility.SaveFolderPanel("Save AssetBundles to directory...", "", "");
            if (string.IsNullOrEmpty(savePath))
            {
                EditorUtility.DisplayDialog("Abundables Exporter", "Error: Invalid Directory!", "OK");
                return;
            }

            var buildData = new List<AssetBundleBuild>(bundles.Length);

            for (int i = 0; i < bundles.Length; i++)
            {
                if (bundles[i] == null)
                {
                    Debug.LogError($"Bundle with index '{i}' is null! Skipping...");
                    continue;
                }

                if (bundles[i].entries.Count == 0)
                {
                    Debug.LogWarning($"There are no assets in bundle '{bundles[i].name}', skipping...");
                    continue;
                }

                var b = new AssetBundleBuild
                {
                    assetBundleName = bundles[i].name,
                    assetNames = bundles[i].GetAllAssetPaths(),
                    addressableNames = bundles[i].GetAllAddresses(),
                };

                buildData.Add(b);
            }

            if (!Directory.Exists(bundleCachePath))
            {
                Directory.CreateDirectory(bundleCachePath);
            }

            BuildPipeline.BuildAssetBundles(bundleCachePath, buildData.ToArray(),
                                            BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);

            //EditorUserBuildSettings.SwitchActiveBuildTarget(EditorUserBuildSettings.selectedBuildTargetGroup, EditorUserBuildSettings.activeBuildTarget);

            for (int i = 0; i < buildData.Count; i++)
            {
                string destPath = Path.Combine(savePath, buildData[i].assetBundleName);
                string srcPath = Path.Combine(bundleCachePath, buildData[i].assetBundleName);

                File.Copy(srcPath, destPath, true);
            }

            EditorUtility.DisplayDialog("Abundables Exporter", "Export Successful!", "OK");
        }

    }
}
