using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Abundables
{
    public class AbundablesWindow : EditorWindow
    {
        private Bundle openedBundle;
        private static AbundableData data;

        private const string dataPath = "Assets/Abundables/Editor/Data/AbundableData.asset";

        private float sideBtnWidth = 26f;
        private float toolbarHeight = 20f;
        private Color lineColor = new Color(35f / 255f, 35f / 255f, 35f / 255f);

        private List<bool> selectedBundles = new();


        #region Window Functions

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

        public void GetSelectedBundles()
        {
            throw new NotImplementedException();
        }

        #endregion

        private void OnGUI()
        {
            DrawBundlesPanel();
            DrawAssetsPanel();

            EditorGUI.DrawRect(new Rect(position.width / 3f, 0f, 1f, position.height), lineColor);
            EditorGUI.DrawRect(new Rect((position.width / 3f) * 2f - 1f, toolbarHeight, 1f, position.height), lineColor);
            EditorGUI.DrawRect(new Rect(position.width / 3f, toolbarHeight, (position.width / 3f) * 2f, 1f), lineColor);
        }

        private void DrawBundlesPanel()
        {
            var bpRect = new Rect(0f, 0f, position.width / 3f, position.height);
            GUILayout.BeginArea(bpRect, FullEditorStyles.Toolbar);

            #region Toolbar

            EditorGUI.DropdownButton(new Rect(0f, 0f, sideBtnWidth, toolbarHeight), EditorGUIUtility.TrIconContent("d_Toolbar Plus", "Create new Bundle"), FocusType.Passive, FullEditorStyles.Toolbarbutton);
            EditorGUI.LabelField(new Rect(sideBtnWidth, 0f, bpRect.width - sideBtnWidth, toolbarHeight), "Bundles", FullEditorStyles.Toolbarbutton.CenterText());

            #endregion

            #region List
            var listRect = new Rect(0, toolbarHeight, bpRect.width, bpRect.height - toolbarHeight * 2);
            {
                GUILayout.BeginArea(listRect);

                var toggleWidth = GUI.skin.toggle.CalcSize(new GUIContent("")).x;
                var togglePosX = (sideBtnWidth / 2f) - (toggleWidth / 2f);

                // Use one of the GUI.Scroll funcs
                //GUI.BeginScrollView(listRect, default(Vector2), default(Rect), false, true);


                var bundles = data.GetBundles();
                for (int i = 0; i < bundles.Length; i++)
                {
                    var yPos = toolbarHeight * i;

                    bool toggleVal = false;
                    try
                    {
                        toggleVal = selectedBundles[i];
                    }
                    catch
                    {
                        selectedBundles.Insert(i, toggleVal);
                    }

                    selectedBundles[i] = EditorGUI.Toggle(new Rect(togglePosX, yPos, sideBtnWidth, toolbarHeight), toggleVal);

                    var oldCol = GUI.color;

                    if(openedBundle == bundles[i])
                    {
                        GUI.color = Color.cyan;
                    }

                    if(GUI.Button(new Rect(sideBtnWidth, yPos, listRect.width - sideBtnWidth, toolbarHeight), bundles[i].name, FullEditorStyles.MiniButton.CenterText()))
                    {
                        openedBundle = bundles[i];
                    }

                    GUI.color = oldCol;
                }


                GUILayout.EndArea();
            }
            #endregion

            #region Build buttons
            {
                GUILayout.BeginArea(new Rect(0, listRect.height + toolbarHeight, bpRect.width, toolbarHeight));

                var bStyle = FullEditorStyles.Toolbarbutton.CenterText();

                List<string> selectBtns = new() { "All", "None" };
                var bRects = EditorGUIUtility.GetFlowLayoutedRects(new Rect(0f, 0f, bpRect.width / 2f, toolbarHeight), bStyle, 0f, 0f, selectBtns);

                if(GUI.Button(bRects[0], new GUIContent(selectBtns[0], "Select all bundles for build"), bStyle))
                {
                    for (int i = 0; i < selectedBundles.Count; i++)
                    {
                        selectedBundles[i] = true;
                    }
                }

                if(GUI.Button(bRects[1], new GUIContent(selectBtns[1], "Deselect all bundles for build"), bStyle))
                {
                    for (int i = 0; i < selectedBundles.Count; i++)
                    {
                        selectedBundles[i] = false;
                    }
                }

                bool anySelected = selectedBundles.Any(sb => sb == true);
                var buildBtnPosX = bRects[0].width + bRects[1].width;

                EditorGUI.BeginDisabledGroup(!anySelected && openedBundle == null);

                string btnTxt = anySelected ? "Build Selected" : "Build Current";
                if(GUI.Button(new Rect(buildBtnPosX, 0f, bpRect.width - buildBtnPosX, toolbarHeight), btnTxt, bStyle))
                {
                    if (anySelected)
                    {
                        var selected = new List<Bundle>();
                        var bundles = data.GetBundles();

                        for (int i = 0; i < selectedBundles.Count; i++)
                        {
                            if (selectedBundles[i])
                                selected.Add(bundles[i]);
                        }

                        AbundablesBuilder.BuildBundles(selected.ToArray());
                    }
                    else
                    {
                        AbundablesBuilder.BuildBundle(openedBundle);
                    }
                }

                EditorGUI.EndDisabledGroup();

                GUILayout.EndArea();
            }
            #endregion

            GUILayout.EndArea();
        }

        private void DrawAssetsPanel()
        {
            var apRect = new Rect(position.width / 3f, 0f, position.width - (position.width / 3f), position.height);
            GUILayout.BeginArea(apRect, FullEditorStyles.Toolbar);

            EditorGUI.LabelField(new Rect(0f, 0f, apRect.width, toolbarHeight), "Assets", FullEditorStyles.Toolbarbutton.CenterText());

            //GUILayout.BeginArea(new Rect(0f, toolbarHeight, apRect.width, toolbarHeight));

            EditorGUI.LabelField(new Rect(0f, toolbarHeight, apRect.width / 2f, toolbarHeight), "Address", FullEditorStyles.Toolbarbutton.CenterText());
            EditorGUI.LabelField(new Rect(apRect.width / 2f, toolbarHeight, apRect.width / 2f, toolbarHeight), "Asset Path", FullEditorStyles.Toolbarbutton.CenterText());

            //GUILayout.EndArea();

            if(openedBundle == null)
            {
                GUILayout.EndArea();
                return;
            }


            var startY = toolbarHeight * 2f;
            for (int i = 0; i < openedBundle.entries.Count; i++)
            {
                var entry = openedBundle.entries[i];
                entry.address = EditorGUI.DelayedTextField(new Rect(0f, startY + (toolbarHeight * i), apRect.width / 2f, toolbarHeight), entry.address).ToLower();

                EditorGUI.SelectableLabel(new Rect(apRect.width / 2f, startY + (toolbarHeight * i), apRect.width / 2f, toolbarHeight), entry.assetPath);
                //EditorGUI.ObjectField(new Rect(apRect.width / 2f, startY + (toolbarHeight * i), apRect.width / 2f, toolbarHeight), new UnityEngine.Object(), typeof(UnityEngine.Object), false);
            }



            GUILayout.EndArea();
        }

        
    }
}
