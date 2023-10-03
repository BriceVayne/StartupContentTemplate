using UnityEditor;
using UnityEngine;

namespace SCT
{
    public partial class StartupContentTemplateWindow
    {
        private static readonly string WINDOW_TITLE = "Startup Content";
        private static readonly Vector2 WINDOW_START_SIZE = new Vector2(300, 600);
        private static readonly Vector2 WINDOW_START_POS = new Vector2(400, 0);

        [MenuItem("Tools/Startup Content Template")]
        public static void ShowWindow()
        {
            StartupContentTemplateWindow window = GetWindow<StartupContentTemplateWindow>();
            window.titleContent = new GUIContent(WINDOW_TITLE);
            window.minSize = WINDOW_START_SIZE;
            window.position = new Rect(WINDOW_START_POS, WINDOW_START_SIZE);
        }
    }
}