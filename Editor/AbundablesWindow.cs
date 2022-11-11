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

        private List<bool> selectedBundles = new List<bool>();


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

            // Create new bundle
            var createBundleRect = new Rect(0f, 0f, sideBtnWidth, toolbarHeight);
            if (EditorGUI.DropdownButton(createBundleRect, EditorGUIUtility.TrIconContent("d_Toolbar Plus", "Create new Bundle"), FocusType.Passive, FullEditorStyles.Toolbarbutton))
            {
                PopupWindow.Show(createBundleRect, new CreateBundlePopup(ref data));
            }
            //if (Event.current.type == EventType.Repaint) 
            //    createBundleRect = GUILayoutUtility.GetLastRect();

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

                for (int i = 0; i < data.bundles.Count; i++)
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

                    if(openedBundle == data.bundles[i])
                    {
                        GUI.color = Color.cyan;
                    }

                    // Bundle Select button
                    if(GUI.Button(new Rect(sideBtnWidth, yPos, listRect.width - sideBtnWidth * 2f, toolbarHeight), data.bundles[i].name, FullEditorStyles.MiniButton.CenterText()))
                    {
                        openedBundle = data.bundles[i];
                    }

                    GUI.color = oldCol;

                    // Delete button
                    if (EditorGUI.DropdownButton(new Rect(listRect.width - sideBtnWidth, yPos, sideBtnWidth, toolbarHeight),
                    EditorGUIUtility.TrIconContent("d_Toolbar Minus", "Delete Bundle"), FocusType.Passive, FullEditorStyles.Toolbarbutton))
                    {
                        Undo.RecordObject(data, "Removed Bundle");

                        if (openedBundle == data.bundles[i])
                            openedBundle = null;

                        data.bundles.RemoveAt(i--);
                    }
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
                        var bundles = data.bundles;

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
            EditorGUI.LabelField(new Rect(apRect.width / 2f, toolbarHeight, apRect.width / 2f - sideBtnWidth, toolbarHeight), "Asset Path", FullEditorStyles.Toolbarbutton.CenterText());

            //GUILayout.EndArea();

            if (openedBundle == null)
            {
                GUILayout.EndArea();
                return;
            }

            // Add Asset button
            if (EditorGUI.DropdownButton(new Rect(apRect.width - sideBtnWidth, toolbarHeight, sideBtnWidth, toolbarHeight),
                    EditorGUIUtility.TrIconContent("d_Toolbar Plus", "Add Asset"), FocusType.Passive, FullEditorStyles.Toolbarbutton))
            {
                openedBundle.entries.Add(new Bundle.Entry());
            }

            var startY = toolbarHeight * 2f;
            for (int i = 0; i < openedBundle.entries.Count; i++)
            {
                var entry = openedBundle.entries[i];
                
                var oldCol = GUI.color;

                if (string.IsNullOrWhiteSpace(entry.address))
                {
                    GUI.color = Color.yellow;
                    entry.address = "";
                }

                entry.address = EditorGUI.TextField(new Rect(0f, startY + (toolbarHeight * i), apRect.width / 2f, toolbarHeight), 
                    entry.address).ToLower().Trim();

                GUI.color = oldCol;

                string assetPath = entry.GetAssetPath();
                if (string.IsNullOrEmpty(assetPath))
                {
                    assetPath = "No Asset Path";
                    GUI.color = Color.yellow;
                }

                //EditorGUI.PrefixLabel(new Rect(apRect.width / 2f, startY + (toolbarHeight * i), apRect.width * 0.2f, toolbarHeight), new GUIContent(entry.GetAssetPath()), FullEditorStyles.ObjectField);

                EditorGUI.SelectableLabel(new Rect(apRect.width / 2f, startY + (toolbarHeight * i), apRect.width * 0.25f, toolbarHeight), assetPath);
                entry.assetObject = EditorGUI.ObjectField(
                    new Rect(apRect.width * 0.75f, startY + (toolbarHeight * i), apRect.width * 0.25f - sideBtnWidth, toolbarHeight), 
                    entry.assetObject, typeof(UnityEngine.Object), false);

                GUI.color = oldCol;

                if(EditorGUI.DropdownButton(new Rect(apRect.width - sideBtnWidth, startY + (toolbarHeight * i), sideBtnWidth, toolbarHeight), 
                    EditorGUIUtility.TrIconContent("d_Toolbar Minus", "Remove Asset"), FocusType.Passive, FullEditorStyles.Toolbarbutton))
                {
                    Undo.RecordObject(data, "Removed Asset Entry");
                    openedBundle.entries.RemoveAt(i--);
                }
            }

            GUILayout.EndArea();
        }

        private class CreateBundlePopup : PopupWindowContent
        {
            private string bundleName = "new.bundle";
            private readonly AbundableData data;

            public CreateBundlePopup(ref AbundableData data) : base()
            {
                this.data = data;
            }

            public override void OnGUI(Rect rect)
            {
                GUILayout.Label("New Bundle Name", FullEditorStyles.BoldLabel);
                bundleName = EditorGUILayout.TextField(bundleName);

                if(GUILayout.Button("Create Bundle"))
                {
                    data.bundles.Add(new Bundle(bundleName));
                    editorWindow.Close();
                }
            }
        }
    }
}
