using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Abundables
{
    public class AbundableData : ScriptableObject
    {
        public List<Bundle> bundles = new();

        public Bundle[] GetBundles() 
        {
            return bundles.ToArray();
        }

        public void BuildBundle(Bundle bundle)
        {
            BuildBundles(new Bundle[] { bundle });
        }

        public void BuildBundles(Bundle[] bundles)
        {
            if(bundles == null || bundles.Length == 0)
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
                if(bundles[i] == null)
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

            Debug.Log("Building bundles to " + savePath);

            // @Todo(Parzival): Not sure if building to tempCache is necessary.
            var manifest = BuildPipeline.BuildAssetBundles(/*Application.temporaryCachePath*/ savePath, buildData.ToArray(),
                                            BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);

            EditorUserBuildSettings.SwitchActiveBuildTarget(EditorUserBuildSettings.selectedBuildTargetGroup, EditorUserBuildSettings.activeBuildTarget);

            //for (int i = 0; i < buildData.Count; i++)
            //{
            //    string filePath = Path.Combine(savePath, buildData[i].assetBundleName);
                
            //    if (File.Exists(filePath))
            //        File.Delete(filePath);

            //    File.Move(Path.Combine(Application.temporaryCachePath, buildData[i].assetBundleName), filePath);
            //}
            
            EditorUtility.DisplayDialog("Abundables Exporter", "Export Successful!", "OK");
        }

        public void ImportExistingAssetBundles()
        {
            string[] bNames = AssetDatabase.GetAllAssetBundleNames();

            foreach (string bName in bNames)
            {
                if(bundles.Any(b => b.name == bName))
                {
                    continue;
                }

                string[] paths = AssetDatabase.GetAssetPathsFromAssetBundle(bName);

                var bundle = new Bundle
                {
                    name = bName,
                    entries = new List<Bundle.Entry>(paths.Length)
                };

                for (int i = 0; i < paths.Length; i++)
                {
                    bundle.entries.Insert(i, new Bundle.Entry { address = paths[i], assetPath = paths[i] });
                }

                bundles.Add(bundle);
            }
        }
    }

    [Serializable]
    public class Bundle
    {
        public string name;
        public List<Entry> entries;

        public string[] GetAllAssetPaths()
        {
            var result = new string[entries.Count];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = entries[i].assetPath;
            }

            return result;
        }

        public string[] GetAllAddresses()
        {
            var result = new string[entries.Count];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = entries[i].address;
            }

            return result;
        }

        [Serializable]
        public class Entry
        {
            public string assetPath;
            public string address;
        }
    }

}
