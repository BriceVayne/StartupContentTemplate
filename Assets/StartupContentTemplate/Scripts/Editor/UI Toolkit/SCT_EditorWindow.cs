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
        [SerializeField] private VisualTreeAsset m_VisualTreeAsset = default;
        [SerializeField] private StyleSheet m_StyleSheet = default;

        private static readonly string WINDOW_TITLE = "Startup Content";
        private static readonly Vector2 WINDOW_START_SIZE = new Vector2(300, 600);
        private static readonly Vector2 WINDOW_START_POS = new Vector2(400, 0);

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
            VisualElement root = rootVisualElement;

            m_ObjectField = new ObjectField("Scriptable Datas");

            m_ObjectField.RegisterCallback<ChangeEvent<Object>>((evt) =>
            {
                if (evt.newValue.GetType() != typeof(ScriptableDatas))
                {
                    m_ObjectField.value = null;
                    PrintMessage("Only SCT_ScriptableDatas supported", HelpBoxMessageType.Warning);
                }
            });

            root.Add(m_ObjectField);

            var mainTitle = new TitleElement("Folders");
            root.Add(mainTitle);


            m_HelpBox = new HelpBox("This is a help box", HelpBoxMessageType.Info);
            root.Add(m_HelpBox);
        }

        private void PrintMessage(string message, HelpBoxMessageType messageType)
        {
            if (m_HelpBox != null)
            {
                m_HelpBox.text = message;
                m_HelpBox.messageType = messageType;
            }
        }
    }
}





