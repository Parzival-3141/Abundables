using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Abundables
{
    public class AbundablesWindow : EditorWindow
    {
        private string currentBundleName;

        [MenuItem("Abundables/Main")]
        public static void ShowWindow()
        {
            var window = GetWindow<AbundablesWindow>("Abundables!");
            window.Show();
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

        private void DrawBundlesPanel()
        {
            GUILayout.Label("Bundles");

            EditorGUILayout.BeginVertical();

            string[] bundleNames = AssetDatabase.GetAllAssetBundleNames();
            foreach (string bName in bundleNames)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.Toggle(false, GUILayout.ExpandWidth(true));
                
                if (GUILayout.Button(bName))
                {
                    currentBundleName = bName;
                }

                EditorGUILayout.EndHorizontal();
            }

            GUILayout.FlexibleSpace();

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.Toggle(false);
            GUILayout.Button("Build Selected");

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();
        }

        private void DrawAssetsPanel()
        {
            GUILayout.Label("Assets");

            EditorGUILayout.BeginHorizontal();


            string[] assetPaths = AssetDatabase.GetAssetPathsFromAssetBundle(currentBundleName);

            // Addresses
            EditorGUILayout.BeginVertical(EditorStyles.helpBox,  GUILayout.ExpandHeight(true));

            GUILayout.Label("Address");
            for (int i = 0; i < assetPaths.Length; i++)
            {
                _ = EditorGUILayout.DelayedTextField("", GUILayout.MinWidth(100f));
            }

            EditorGUILayout.EndVertical();

            // Asset Paths
            EditorGUILayout.BeginVertical(EditorStyles.helpBox,  GUILayout.ExpandHeight(true));

            GUILayout.Label("Asset Path");
            foreach(string path in assetPaths)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label(path);
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
