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
    public class SCT_EditorWindow : EditorWindow
    {
        [SerializeField] private StyleSheet m_LightStyleSheet = default;
        [SerializeField] private StyleSheet m_DarkStyleSheet = default;

        private static readonly string WINDOW_TITLE = "Startup Content";
        private static readonly string ASSETS_PATH_EDITOR = Application.dataPath;
        private static readonly Vector2 WINDOW_START_SIZE = new Vector2(300, 600);
        private static readonly Vector2 WINDOW_START_POS = new Vector2(400, 0);

        private StyleSheet currentStyleSheet;
        private VisualElement m_Header;
        private VisualElement m_Body;
        private VisualElement m_Footer;
        private ObjectField m_ObjectField;
        private MultiColumnTreeView m_TreeView;
        private HelpBox m_HelpBox;
        private List<Folder> m_Directories;

        [MenuItem("Tools/Startup Content Template")]
        public static void ShowWindow()
        {
            SCT_EditorWindow window = GetWindow<SCT_EditorWindow>();
            window.titleContent = new GUIContent(WINDOW_TITLE);
            window.minSize = WINDOW_START_SIZE;
            window.position = new Rect(WINDOW_START_POS, WINDOW_START_SIZE);
        }

        private void OnEnable()
        {
            m_Directories = new List<Folder>();
        }

        public void CreateGUI()
        {
            currentStyleSheet = EditorGUIUtility.isProSkin ? m_DarkStyleSheet : m_LightStyleSheet;

            CreateHierarchy();

            m_Header.Add(CreateTitle("Datas"));
            m_Header.Add(CreateAndBindObjectField());

            m_Body.Add(CreateTitle("Folders"));

            m_Footer.Add(CreateHelpBox());
        }

        private void CreateHierarchy()
        {
            m_Header = CreateBlock();
            m_Body = CreateBlock();
            m_Footer = CreateBlock();

            rootVisualElement.name = "#RootBackground";
            rootVisualElement.Add(m_Header);
            rootVisualElement.Add(m_Body);
            rootVisualElement.Add(m_Footer);
        }

        private void PrintMessage(string message, HelpBoxMessageType messageType)
        {
            if (m_HelpBox != null)
            {
                m_HelpBox.text = message;
                m_HelpBox.messageType = messageType;
            }
        }

        private VisualElement CreateBlock()
        {
            var part = new VisualElement();
            part.name = "Block";
            part.styleSheets.Add(currentStyleSheet);
            return part;
        }

        private VisualElement CreateTitle(string title)
        {
            var titleElement = new TitleElement(title);
            titleElement.styleSheets.Add(currentStyleSheet);
            return titleElement;
        }

        private VisualElement CreateAndBindObjectField()
        {
            m_ObjectField = new ObjectField("Scriptable Datas Asset");
            m_ObjectField.name = "Space";
            m_ObjectField.styleSheets.Add(currentStyleSheet);
            m_ObjectField.objectType = typeof(ScriptableDatas);
            m_ObjectField.RegisterValueChangedCallback((callback) =>
            {
                m_Directories.Clear();

                var result = callback.newValue as ScriptableDatas;
                if (result != null)
                {
                    m_Directories = result.Foolders;
                    m_Body.Add(CreateTreeView());
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
            m_TreeView.styleSheets.Add(currentStyleSheet);
            m_TreeView.SetRootItems(roots);

            m_TreeView.columns["FolderName"].makeCell = () =>
            {
                VisualElement visualElement = new VisualElement();
                visualElement.name = "ItemView";

                Image img = new Image();
                img.name = "Icon";
                img.styleSheets.Add(currentStyleSheet);

                Label label = new Label();
                label.name = "Label";
                label.styleSheets.Add(currentStyleSheet);

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