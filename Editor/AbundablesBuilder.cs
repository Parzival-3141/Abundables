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
            bool hadErrors = false;
            for (int i = 0; i < bundles.Length; i++)
            {
                if (bundles[i] == null)
                {
                    Debug.LogError($"Bundle with index '{i}' is null! Skipping...");
                    hadErrors = true;
                    continue;
                }

                if (bundles[i].entries.Count == 0)
                {
                    Debug.LogWarning($"There are no Assets in Bundle '{bundles[i].name}', skipping...");
                    hadErrors = true;
                    continue;
                }

                var paths = new List<string>();
                var addresses = new List<string>();

                for (int j = 0; j < bundles[i].entries.Count; j++)
                {
                    var entry = bundles[i].entries[j];
                    if(entry == null || entry.assetObject == null || string.IsNullOrWhiteSpace(entry.address))
                    {
                        Debug.LogWarning($"Invalid Asset '{j}' in Bundle '{bundles[i].name}, skipping...'");
                        hadErrors = true;
                        continue;
                    }

                    paths.Add(entry.GetAssetPath());
                    addresses.Add(entry.address);
                }

                if(paths.Count < 1 || addresses.Count < 1)
                {
                    Debug.LogWarning($"There are no valid Assets in Bundle '{bundles[i].name}, skipping...'");
                    hadErrors = true;
                    continue;
                }

                var b = new AssetBundleBuild
                {
                    assetBundleName = bundles[i].name,
                    assetNames = paths.ToArray(),
                    addressableNames = addresses.ToArray(),
                };

                buildData.Add(b);
            }

            if(buildData.Count < 1)
            {
                EditorUtility.DisplayDialog("Abundables Exporter", "Error: No valid Bundles to build!\nAborting, see Console for more details.", "OK");
                return;
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

            if(hadErrors)
                EditorUtility.DisplayDialog("Abundables Exporter", "Export Successful, but some Bundles had problems! See Console for more details.", "OK");
            else
                EditorUtility.DisplayDialog("Abundables Exporter", "Export Successful!", "OK");
        }

    }
}
