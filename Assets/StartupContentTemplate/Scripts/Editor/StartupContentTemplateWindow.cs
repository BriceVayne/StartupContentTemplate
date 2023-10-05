using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace SCT
{
    /// <summary>
    /// The SCT editor window
    /// </summary>
    public partial class StartupContentTemplateWindow : EditorWindow
    {
        [SerializeField] private StyleSheet m_StyleSheet = default;

        private static readonly string ASSETS_PATH_EDITOR = Application.dataPath;

        private List<TreeViewItemData<Folder>> m_TreeViewItemDatas;

        private VisualElement m_Header;
        private VisualElement m_Body;
        private VisualElement m_Footer;


        private ObjectField m_ObjectField;
        private MultiColumnTreeView m_TreeView;
        private HelpBox m_HelpBox;


        public void CreateGUI()
        {
            SetupRoot();
            SetupHeader();
            SetupBody();
            SetupFooter();
        }

        private void SetupRoot()
        {
            CreateBlocks();

            rootVisualElement.name = "Main";
            rootVisualElement.styleSheets.Add(m_StyleSheet);
            rootVisualElement.Add(m_Header);
            rootVisualElement.Add(m_Body);
            rootVisualElement.Add(m_Footer);
        }

        private void CreateBlocks()
        {
            m_Header = SCT_Library.Create<VisualElement>("Block", m_StyleSheet);
            m_Body = SCT_Library.Create<VisualElement>("Block", m_StyleSheet);
            m_Footer = SCT_Library.Create<VisualElement>("Block", m_StyleSheet);
        }

        private void SetupHeader()
        {
            m_Header.Add(SCT_Library.Create("Datas", "Title", m_StyleSheet));
            m_Header.Add(CreateAndBindObjectField(typeof(ScriptableDatas), "Scriptable Datas Asset"));
            //m_Header.Add(CreateAndBindObjectField(typeof(DefaultAsset), "Root Folder"));
        }

        private void SetupBody()
        {
            m_Body.Add(SCT_Library.Create("Folders", "Title", m_StyleSheet));
            m_Body.Add(CreateTreeView());
        }

        private VisualElement CreateAndBindObjectField(Type objectType, string label)
        {
            m_ObjectField = SCT_Library.Create(objectType, label, "Space", m_StyleSheet);
            m_ObjectField.RegisterValueChangedCallback(BindObjectField);
            return m_ObjectField;
        }

        private void BindObjectField(ChangeEvent<Object> callback)
        {
            var result = callback.newValue as ScriptableDatas;
            if (result != null)
                UpdateTreeViewItemData(result.Foolders);
        }

        // Only 2 Deep
        private void UpdateTreeViewItemData(List<Folder> groupOrFolder)
        {
            int id = 0;
            m_TreeViewItemDatas = new List<TreeViewItemData<Folder>>(groupOrFolder.Count); // Main list

            foreach (var folders in groupOrFolder) // Run through the main list
            {
                // For each elem, create new Item Data to subfolder
                var foldersInGroup = new List<TreeViewItemData<Folder>>(folders.Content.Count);
                foreach (var folder in folders.Content) // Run through the sub list
                    foldersInGroup.Add(new TreeViewItemData<Folder>(id++, folder)); // Create elem into sublist

                // Add sublist to main list
                m_TreeViewItemDatas.Add(new TreeViewItemData<Folder>(id++, folders, foldersInGroup));
            }

            m_TreeView.SetRootItems(m_TreeViewItemDatas);
        }

        private Columns CreateTreeViewColumns()
        {
            Columns columns = new Columns();
            Column columnName = new Column() { name = "FolderName", stretchable = true, minWidth = 100, sortable = true, title = "Folder Name", };
            Column columnCreate = new Column() { name = "ShouldCreate", stretchable = true, minWidth = 110, maxWidth = 110, sortable = true, title = "Should Create ?" };

            columns.Add(columnName);
            columns.Add(columnCreate);

            return columns;
        }

        private void CreateCells()
        {
            m_TreeView.columns["FolderName"].makeCell = () =>
            { 
                VisualElement visualElement = SCT_Library.Create<VisualElement>("ItemView", m_StyleSheet);
                Image img = SCT_Library.Create<Image>("Icon", m_StyleSheet);
                Label label = SCT_Library.Create<Label>("Label", m_StyleSheet);

                visualElement.Add(img);
                visualElement.Add(label);

                return visualElement;
            };

            m_TreeView.columns["ShouldCreate"].makeCell = () => SCT_Library.Create<Toggle>("Toggle", m_StyleSheet);
        }

        private void BindCells()
        {
            m_TreeView.columns["FolderName"].bindCell = BindFolderCell;
            m_TreeView.columns["ShouldCreate"].bindCell = BindToggleCell;
        }

        private void BindFolderCell(VisualElement element, int index)
        {
            Texture icon = FindFolderTexture(index);

            element.Q<Image>().style.backgroundImage = (StyleBackground)icon;
            element.Q<Label>().text = m_TreeView.GetItemDataForIndex<Folder>(index).Name;
        }

        private Texture FindFolderTexture(int index)
        {
            Texture icon;
            if (!m_TreeView.viewController.HasChildrenByIndex(index))
                icon = EditorGUIUtility.IconContent("FolderEmpty On Icon").image;
            else
            {
                if (m_TreeView.viewController.IsExpandedByIndex(index))
                    icon = EditorGUIUtility.IconContent("FolderOpened On Icon").image;
                else
                    icon = EditorGUIUtility.IconContent("Folder On Icon").image;
            }

            return icon;
        }

        private void BindToggleCell(VisualElement element, int index)
        {
            (element as Toggle).value = m_TreeView.GetItemDataForIndex<Folder>(index).ShouldCreate;
        }

        private void SetupColumns()
        {
            CreateCells();
            BindCells();
        }

        private VisualElement CreateTreeView()
        {
            m_TreeView = SCT_Library.Create(CreateTreeViewColumns(), "TreeView", m_StyleSheet);
            
            SetupColumns();

            return m_TreeView;
        }

        private void SetupFooter()
        {
            m_Footer.Add(SCT_Library.Create("Options", "Title", m_StyleSheet));
            m_Footer.Add(SCT_Library.CreateToggle("Git compatibility", "Toggle", m_StyleSheet));
            m_Footer.Add(SCT_Library.CreateButton("Create Folder(s)", "Button", m_StyleSheet));
        }
    }
}