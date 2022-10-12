using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;


/*  @Todo: rewrite this whole ass thing.
    Needs to be simplified.
    
    The point of this is to have an easy medium between bare AssetBundles
    and the overcomplicated Addressables. You should have decent control
    with just a few clicks.

    Users should be able to:
        - Select Assets to include in a bundle
        - Create aliases for those Assets that can be used at runtime
        - Build selected bundles
   

*/


namespace Abundables
{
    using Object = UnityEngine.Object;

    public static class Database
    {
        public static List<Bundle> bundles = new();
        public static Bundle currentBundle;

        public static void ChangeCurrentBundle(Bundle bundle)
        {
            if(bundle == null)
            {
                Debug.LogWarning("Bundle is null!");
                return;
            }

            DatabaseWindow.treeDirty = true;
            currentBundle = bundle;
        }


        public static bool TryCreateBundle(string name, out Bundle bundle, string postfix = "")
        {
            bundle = null;
            bool result = false;

            if(!bundles.Any(b => b.name == name))
            {
                bundle = new Bundle(name, postfix);
                result = true;
            }

            return result;
        }

        public static void AddToCurrentBundle(BundleEntry entry)
        {
            if(bundles.Count < 1)
            {
                bundles.Add(new Bundle("Default Bundle"));
                currentBundle = bundles[0];
            }

            currentBundle.TryAdd(entry);
            DatabaseWindow.treeDirty = true;
        }

        public static bool Contains(string assetGuid, out Bundle bundle, out BundleEntry entry)
        {
            bool result = false;

            bundle = null;
            entry = null;

            for (int i = 0; i < bundles.Count; i++)
            {
                entry = bundles[i].entries.Find(e => e.guid == assetGuid);
                if (entry != null)
                {
                    result = true;
                    bundle = bundles[i];
                    break;
                }
            }

            return result;
        }
    }
    public class Bundle
    {
        public string name;
        public string postfix;

        public List<BundleEntry> entries = new List<BundleEntry>();

        public Bundle(string name, string postfix = "")
        {
            this.name = name;
            this.postfix = postfix;
        }

        public bool TryAdd(BundleEntry entry)
        {
            bool result = false;

            if(!entries.Any(e => e.guid == entry.guid))
            {
                result = true;
                entries.Add(entry);
                DatabaseWindow.treeDirty = true;
            }

            return result;
        }

        public bool Remove(BundleEntry entry)
        {
            bool result = entries.Remove(entry);
            
            if(result)
                DatabaseWindow.treeDirty = true;
            
            return result;
        }
    }

    public class BundleEntry
    {
        public string assetPath;
        public string bundleAddress;
        public string guid;

        public BundleEntry(Object obj, string assetPath, string abundleAddress)
        {
            this.assetPath = assetPath;
            AssetDatabase.TryGetGUIDAndLocalFileIdentifier(obj, out guid, out long _);
            this.bundleAddress = abundleAddress;
        }

        public BundleEntry(string assetPath, string abundleAddress, string guid)
        {
            this.assetPath = assetPath;
            this.bundleAddress = abundleAddress;
            this.guid = guid;
        }
    }
}
