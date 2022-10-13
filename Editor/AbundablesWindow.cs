using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Abundables
{
    public class AbundablesWindow : EditorWindow
    {
        private Bundle openedBundle;
        private static AbundableData data;

        private const string dataPath = "Assets/Abundables/Editor/Data/AbundableData.asset";

        [InitializeOnLoadMethod]
        private static void OnLoad()
        {
            if(data == null)
            {
                data = AssetDatabase.LoadAssetAtPath<AbundableData>(dataPath);

                if(data == null)
                {
                    data = CreateInstance<AbundableData>();
                    AssetDatabase.CreateAsset(data, dataPath);
                    AssetDatabase.Refresh();
                }
            }
        }

        [MenuItem("Abundables/Open Editor")]
        public static void ShowWindow()
        {
            var window = GetWindow<AbundablesWindow>("Abundables!");
            window.Show();
        }

        [MenuItem("Abundables/Import Existing AssetBundles")]
        public static void ImportExistingBundles()
        {
            if(data != null)
            {
                data.ImportExistingAssetBundles();
            }
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            
            EditorGUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.MaxWidth(150f), GUILayout.ExpandHeight(true));
            DrawBundlesPanel();
            EditorGUILayout.EndVertical();
            
            
            EditorGUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.ExpandHeight(true));
            DrawAssetsPanel();
            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();
        }

        public void GetSelectedBundles()
        {
            throw new NotImplementedException();
        }

        private void DrawBundlesPanel()
        {
            GUILayout.Label("Bundles");

            EditorGUILayout.BeginVertical();

            //string[] bundleNames = AssetDatabase.GetAllAssetBundleNames();
            
            foreach (var bundle in data.GetBundles())
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.Toggle(false, GUILayout.ExpandWidth(true));
                
                if (GUILayout.Button(bundle.name))
                {
                    openedBundle = bundle;
                }

                EditorGUILayout.EndHorizontal();
            }

            GUILayout.FlexibleSpace();

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.Toggle(false);

            if(GUILayout.Button("Build Selected"))
            {
                data.BuildBundle(openedBundle);
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();
        }

        private void DrawAssetsPanel()
        {
            GUILayout.Label("Assets");

            if(openedBundle == null)
            {
                return;
            }

            EditorGUILayout.BeginHorizontal();


            // Addresses
            EditorGUILayout.BeginVertical(EditorStyles.helpBox,  GUILayout.ExpandHeight(true));

            GUILayout.Label("Address");
            for (int i = 0; i < openedBundle.entries.Count; i++)
            {
                openedBundle.entries[i].address = EditorGUILayout.DelayedTextField(openedBundle.entries[i].address, GUILayout.MinWidth(100f));
            }

            EditorGUILayout.EndVertical();

            // Asset Paths
            EditorGUILayout.BeginVertical(EditorStyles.helpBox,  GUILayout.ExpandHeight(true));

            GUILayout.Label("Asset Path");
            foreach(var e in openedBundle.entries)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label(e.assetPath);
                //GUILayout.FlexibleSpace();
                //EditorGUIUtility.
                //EditorGUILayout.ObjectField(obj: AssetDatabase.LoadMainAssetAtPath(path), typeof(UnityEngine.Object), false);
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();
            
            EditorGUILayout.EndHorizontal();
        }
    }
}
