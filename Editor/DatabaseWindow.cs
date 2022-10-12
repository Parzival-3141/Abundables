using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Abundables
{
    public class DatabaseWindow : EditorWindow
    {
        public static bool treeDirty = false;

        private static DatabaseWindow window;
        
        [SerializeField] private TreeViewState treeState;
        private BundleEntryTree dbTree;

        private const int toolbarHeight = 20;


        [MenuItem("Abundables/Database Window")]
        private static void ShowWindow()
        {
            window = GetWindow<DatabaseWindow>();
            window.titleContent = new GUIContent("Abundables Database");
            window.Show();
        }

        public static void ShowAbundable(Bundle bundle, BundleEntry entry)
        {
            Database.ChangeCurrentBundle(bundle);
            ShowWindow();
        }

        public static void ReloadTree()
        {
            if(window != null)
            {
                window.dbTree.Reload();
            }
        }

        private void OnEnable()
        {
            if(treeState == null)
            {
                treeState = new TreeViewState();
            }

            dbTree = new BundleEntryTree(treeState);
        }

        private void OnDestroy()
        {
            window = null;
        }

        private void OnGUI()
        {
            if (treeDirty)
            {
                ReloadTree();
                treeDirty = false;
            }

            DrawToolbar(new Rect(position.xMin, position.yMin, position.width, toolbarHeight));
            dbTree.OnGUI(new Rect(position.xMin, position.yMin - toolbarHeight, position.width, position.height /*- toolbarHeight*/));
        }

        private void DrawToolbar(Rect toolbarPos)
        {
            GUILayout.BeginArea(new Rect(0, 0, toolbarPos.width, toolbarHeight));

            GUILayout.BeginHorizontal(EditorStyles.toolbar);
            {

                var content = new GUIContent("Bundle: " + Database.currentBundle?.name ?? "None", "Change selected bundle");
                Rect r = GUILayoutUtility.GetRect(content, EditorStyles.toolbarDropDown, GUILayout.ExpandWidth(false));
                if (EditorGUI.DropdownButton(r, content, FocusType.Passive, EditorStyles.toolbarDropDown))
                {
                    var menu = new GenericMenu();

                    foreach(var bundy in Database.bundles)
                    {
                        if(bundy != null)
                        {
                            menu.AddItem(new GUIContent(bundy.name), false, x => Database.ChangeCurrentBundle(x as Bundle), bundy);
                        }
                    }

                    menu.DropDown(r);
                }

            }
            GUILayout.EndHorizontal();
        }

        private class BundleEntryTree : TreeView
        {
            public BundleEntryTree(TreeViewState state) : base(state)
            {
                Reload();
            }

            protected override TreeViewItem BuildRoot()
            {
                var root = new TreeViewItem { id = 0, depth = -1, displayName = "Root" };
                var allItems = new List<TreeViewItem>();

                for (int i = 0; i < Database.currentBundle.entries.Count; i++)
                {
                    BundleEntry entry = Database.currentBundle.entries[i];
                    allItems.Add(new TreeViewItem(i, 0, $"Address: {entry.bundleAddress} | Path: {entry.assetPath}"));
                }

                SetupParentsAndChildrenFromDepths(root, allItems);
                return root;
            }
        }
    }
}
