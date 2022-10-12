using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Abundables
{
    [InitializeOnLoad]
    public static class InspectorGUI
    {
        //static GUIStyle toggleMixed;
        private static GUIContent toggleText;

        static InspectorGUI()
        {
            toggleText = new GUIContent("Abundable",
                "Check this box to include this asset in an AssetBundle and allow it to be easily loaded via script. " +
                "(This is way easier than Addressables!)");

            Editor.finishedDefaultHeaderGUI += OnPostHeaderGUI;
        }

        private static void OnPostHeaderGUI(Editor editor)
        {
            if (editor.targets.Length > 0)
            {
                if (editor.targets[0].GetType() == typeof(GameObject))
                    return;


                if (editor.targets.Length == 1)
                {
                    string assetPath = AssetDatabase.GetAssetOrScenePath(editor.target);

                    if (!Utils.IsPathValidForEntry(assetPath))
                    {
                        return;
                    }

                    AssetDatabase.TryGetGUIDAndLocalFileIdentifier(editor.target, out string guid, out long _);

                    GUILayout.BeginHorizontal();

                    Bundle bundle;
                    BundleEntry entry;
                    if (Database.Contains(guid, out bundle, out entry))
                    {
                        // if toggle goes true -> false
                        if(!GUILayout.Toggle(true, toggleText, GUILayout.ExpandWidth(false)))
                        {
                            Debug.Log("Remove " + assetPath + " from Database");
                            bundle.Remove(entry);
                            GUIUtility.ExitGUI();
                        }
                    }
                    else
                    {
                        // if toggle goes false -> true
                        if(GUILayout.Toggle(false, toggleText, GUILayout.ExpandWidth(false)))
                        {
                            Debug.Log("Add " + assetPath + " to Database");
                            entry = new BundleEntry(editor.target, assetPath, assetPath);
                            Database.AddToCurrentBundle(entry);
                            bundle = Database.currentBundle;
                        }
                        else
                        {
                            GUILayout.EndHorizontal();
                            return;
                        }
                    }

                    entry.bundleAddress = EditorGUILayout.DelayedTextField(entry.bundleAddress, GUILayout.ExpandWidth(true));

                    if(GUILayout.Button("Show in Editor"))
                    {
                        Debug.Log("Showing " + entry.bundleAddress + " in the Editor");
                        DatabaseWindow.ShowAbundable(bundle, entry);
                    }

                    GUILayout.EndHorizontal();
                }
                else
                {
                    GUILayout.Label("Abundables only supports editing one Object at a time");
                }
            }
        }
    }
}