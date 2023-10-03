using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace SCT
{
    /// <summary>
    /// The SCT editor window
    /// </summary>
    public partial class StartupContentTemplateWindow : EditorWindow
    {
        [SerializeField] private StyleSheet m_StyleSheet = default;

        private static readonly string ASSETS_PATH_EDITOR = Application.dataPath;

        private List<Folder> m_Directories;

        private VisualElement m_Header;
        private VisualElement m_Body;
        private VisualElement m_Footer;


        private ObjectField m_ObjectField;
        private MultiColumnTreeView m_TreeView;
        private HelpBox m_HelpBox;
        

        private void OnEnable()
        {
            m_Directories = new List<Folder>();
        }

        private void OnDisable()
        {
            m_Directories.Clear();
        }

        public void CreateGUI()
        {
            SetupRoot();
            SetupHeader();

            m_Body.Add(SCT_Library.Create("Folders", "Title", m_StyleSheet));

            m_Footer.Add(SCT_Library.Create("Options", "Title", m_StyleSheet));
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
            m_Header.Add(CreateAndBindObjectField());
        }

        private VisualElement CreateAndBindObjectField()
        {
            m_ObjectField = new ObjectField("Scriptable Datas Asset");
            m_ObjectField.name = "Space";
            m_ObjectField.styleSheets.Add(m_StyleSheet);
            m_ObjectField.objectType = typeof(ScriptableDatas);
            m_ObjectField.RegisterValueChangedCallback((callback) =>
            {
                m_Directories.Clear();

                var result = callback.newValue as ScriptableDatas;
                if (result != null)
                {
                    m_Directories = result.Foolders;
                    //m_Body.Add(CreateTreeView());
                }
            });
            return m_ObjectField;
        }

        private VisualElement CreateTreeView()
        {
            int id = 0;
            var roots = new List<TreeViewItemData<Folder>>(m_Directories.Count); // Main list

            foreach (var folders in m_Directories) // Run through the main list
            {
                // For each elem, create new Item Data to subfolder
                var foldersInGroup = new List<TreeViewItemData<Folder>>(folders.Content.Count);
                foreach (var folder in folders.Content) // Run through the sub list
                    foldersInGroup.Add(new TreeViewItemData<Folder>(id++, folder)); // Create elem into sublist

                // Add sublist to main list
                roots.Add(new TreeViewItemData<Folder>(id++, folders, foldersInGroup));
            }

            Columns clmns = new Columns();
            Column name = new Column() { name = "FolderName", stretchable = true, minWidth = 100, sortable = true, title = "Folder Name", };
            Column create = new Column() { name = "ShouldCreate", stretchable = true, minWidth = 110, maxWidth = 110, sortable = true, title = "Should Create ?" };
            clmns.Add(name);
            clmns.Add(create);

            m_TreeView = new MultiColumnTreeView(clmns);
            m_TreeView.name = "TreeView";
            m_TreeView.styleSheets.Add(m_StyleSheet);
            m_TreeView.SetRootItems(roots);

            m_TreeView.columns["FolderName"].makeCell = () =>
            {
                VisualElement visualElement = new VisualElement();
                visualElement.name = "ItemView";

                Image img = new Image();
                img.name = "Icon";
                img.styleSheets.Add(m_StyleSheet);

                Label label = new Label();
                label.name = "Label";
                label.styleSheets.Add(m_StyleSheet);

                visualElement.Add(img);
                visualElement.Add(label);
                return visualElement;
            };
            m_TreeView.columns["ShouldCreate"].makeCell = () =>
            {
                Toggle toggle = new Toggle();
                toggle.name = "Toggle";

                return toggle;
            };

            m_TreeView.columns["FolderName"].bindCell = (VisualElement element, int index) =>
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

                element.Q<Image>().style.backgroundImage = (StyleBackground)icon;
                element.Q<Label>().text = m_TreeView.GetItemDataForIndex<Folder>(index).Name;
            };
            m_TreeView.columns["ShouldCreate"].bindCell = (VisualElement element, int index) =>
            {
                (element as Toggle).value = m_TreeView.GetItemDataForIndex<Folder>(index).ShouldCreate;
            };

            return m_TreeView;
        }

        private VisualElement CreateHelpBox()
        {
            m_HelpBox = new HelpBox("This is a help box", HelpBoxMessageType.Info);
            return m_HelpBox;
        }
    }
}