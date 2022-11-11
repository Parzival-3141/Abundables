using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Abundables
{
    public class AbundableData : ScriptableObject
    {
        public List<Bundle> bundles = new List<Bundle>();

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

                var bundle = new Bundle(bName, new List<Bundle.Entry>(paths.Length));

                for (int i = 0; i < paths.Length; i++)
                {
                    bundle.entries.Insert(i, new Bundle.Entry { address = paths[i], assetObject = AssetDatabase.LoadMainAssetAtPath(paths[i]) });
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

        public Bundle(string name, List<Entry> entries = null)
        {
            this.name = name;
            this.entries = entries == null ? new List<Entry>() : entries;
        }

        public string[] GetAllAssetPaths()
        {
            var result = new string[entries.Count];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = entries[i].GetAssetPath();
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
            //public string assetPath;
            public string address = "";
            public Object assetObject;

            public string GetAssetPath()
            {
                return AssetDatabase.GetAssetPath(assetObject);
            }
        }
    }

}
