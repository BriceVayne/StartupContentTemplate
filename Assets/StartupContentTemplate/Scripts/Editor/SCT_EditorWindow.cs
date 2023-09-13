using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace StartupContentTemplate
{
    /// <summary>
    /// The SCT editor window
    /// </summary>
    public class SCT_EditorWindow : EditorWindow
    {
        private static readonly Vector2 WINDOW_START_SIZE = new Vector2(300, 600);
        private static readonly Vector2 WINDOW_START_POS = new Vector2(400, 0);
        private static readonly string WINDOW_TITLE = "Startup Content";
        private static readonly string ASSETS_PATH_EDITOR = Application.dataPath;
        private const string SETTING_PATH = "Packages/com.brice_vn_unity.startupcontenttemplate/SCT_JsonSettings.json";

        private SCT_JSonContainer jsonRoot;
        private List<SCT_Directory> directories;

        [MenuItem("Tools/Startup Content Template")]
        private static void ShowWindow()
        {
            SCT_EditorWindow window = GetWindow<SCT_EditorWindow>();
            window.titleContent = new GUIContent(WINDOW_TITLE);
            window.minSize = WINDOW_START_SIZE;
            window.position = new Rect(WINDOW_START_POS, WINDOW_START_SIZE);
        }

        private void OnEnable()
        {
            directories = new List<SCT_Directory>();

            string path = Path.GetFullPath(SETTING_PATH);
            var jsonReader = File.ReadAllText(path);
            jsonRoot = JsonUtility.FromJson<SCT_JSonContainer>(jsonReader);
            jsonRoot.Directories.Sort();
            jsonRoot.Directories.ForEach(dirPath => directories.Add(new SCT_Directory(dirPath)));
        }

        private void OnGUI()
        {
            GenerateHeader();
            GenerateBody();
            GenerateFooter();
        }

        private void GenerateHeader()
        {
            
        }

        private void GenerateBody()
        {
            SCT_EditorFunction.DrawTitle("Directories", Color.gray);
            SCT_EditorFunction.DrawToggleDirectories(directories, ASSETS_PATH_EDITOR);

            EditorGUILayout.Space(10f);
            EditorGUILayout.BeginHorizontal();
            var style = new GUIStyle(GUI.skin.button) { alignment = TextAnchor.MiddleCenter };
            if (GUILayout.Button("Select All", style, GUILayout.ExpandWidth(true)))
                SelectAll();
            if (GUILayout.Button("Unselect All", style, GUILayout.ExpandWidth(true)))
                UnSelectAll();
            EditorGUILayout.EndHorizontal();
        }

        private void GenerateFooter()
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.Space();

            if (GUILayout.Button("Generate !"))
            {
                SCT_EditorFunction.CreateDirectories(directories, ASSETS_PATH_EDITOR);
                AssetDatabase.Refresh();
            }

            EditorGUILayout.EndVertical();
        }

        private void SelectAll()
        {
            ToggleSelection(true);
        }

        private void UnSelectAll()
        {
            ToggleSelection(false);
        }

        private void ToggleSelection(bool isSelected)
            => directories.ForEach(dir => dir.ShouldCreate = isSelected);
    }
}
