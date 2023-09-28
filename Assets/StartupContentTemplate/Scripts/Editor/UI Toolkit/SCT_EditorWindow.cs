using System.Runtime.Remoting.Messaging;
using UnityEditor;
using UnityEditor.Search;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using ObjectField = UnityEditor.UIElements.ObjectField;

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
        private static readonly Vector2 WINDOW_START_SIZE = new Vector2(300, 600);
        private static readonly Vector2 WINDOW_START_POS = new Vector2(400, 0);

        private StyleSheet currentStyleSheet;
        private VisualElement m_Header;
        private VisualElement m_Body;
        private VisualElement m_Footer;
        private ObjectField m_ObjectField;
        private HelpBox m_HelpBox;

        [MenuItem("Tools/Startup Content Template")]
        public static void ShowWindow()
        {
            SCT_EditorWindow window = GetWindow<SCT_EditorWindow>();
            window.titleContent = new GUIContent(WINDOW_TITLE);
            window.minSize = WINDOW_START_SIZE;
            window.position = new Rect(WINDOW_START_POS, WINDOW_START_SIZE);
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
            m_ObjectField.name="Space";
            m_ObjectField.styleSheets.Add(currentStyleSheet);
            m_ObjectField.RegisterCallback<ChangeEvent<Object>>((evt) =>
            {
                if (evt.newValue.GetType() != typeof(ScriptableDatas))
                {
                    m_ObjectField.value = null;
                    PrintMessage("Only SCT_ScriptableDatas supported", HelpBoxMessageType.Warning);
                }
            });

            return m_ObjectField;
        }

        private VisualElement CreateHelpBox()
        {
            var helpBox = new HelpBox("This is a help box", HelpBoxMessageType.Info);
            return helpBox;
        }
    }
}