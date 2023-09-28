using SCT;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace StartupContentTemplate
{
    /// <summary>
    /// SCT Function used to make the editor tool.
    /// </summary>
    public static class SCT_Library
    {
        /// <summary>
        /// Draw the toggle list with a label.
        /// </summary>
        /// <param name="list">A directory's list.</param>
        /// <param name="path">The root path where folders should exists. This param is to check if a folder already exist and should be check.</param>
        public static void DrawToggleDirectories(List<Folder> list, string path)
        {
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Folder dir = list[i];
                    string folderPath = path + "/" + dir.Path;
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Space(dir.Depth * 20f);

                    dir.ShouldCreate = EditorGUILayout.Toggle(System.IO.Directory.Exists(folderPath) ? true : dir.ShouldCreate, GUILayout.Width(20f));

                    EditorGUILayout.LabelField(dir.Name);
                    EditorGUILayout.EndHorizontal();
                }
            }
        }

        /// <summary>
        /// Draw a UI line.
        /// </summary>
        /// <param name="color">Line's color.</param>
        /// <param name="thickness">Line's thickness.</param>
        /// <param name="padding">Padding upper and under the line.</param>
        /// <param name="margin">Margin at left and right the line.</param>
        public static void DrawUILine(Color color = default, int thickness = 1, int padding = 20, int margin = 0)
        {
            color = color != default ? color : Color.grey;
            Rect r = EditorGUILayout.GetControlRect(false, GUILayout.Height(padding + thickness));
            r.height = thickness;
            r.y += padding * 0.5f;

            if (margin < 0)
            {
                r.x = 0;
                r.width = EditorGUIUtility.currentViewWidth;
            }
            else
            {
                r.x += margin;
                r.width -= margin * 2;
            }

            EditorGUI.DrawRect(r, color);
        }

        /// <summary>
        /// Used <see cref="DrawUILine"/> to create a graphic title border by lines.
        /// </summary>
        /// <param name="title">Text to display.</param>
        /// <param name="color">Line's color.</param>
        /// <param name="thickness">Line's thickness.</param>
        /// <param name="padding">Padding upper and under the line.</param>
        /// <param name="margin">Margin at left and right the line.</param>
        public static void DrawTitle(string title, Color color, int thickness = 1, int padding = 20, int margin = 0)
        {
            DrawUILine(color, thickness, padding, margin);
            var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };
            EditorGUILayout.LabelField(title, style, GUILayout.ExpandWidth(true));
            DrawUILine(color, thickness, padding, margin);
        }

        /// <summary>
        /// Create directories.
        /// </summary>
        /// <param name="list">A directory's list.</param>
        /// <param name="folderPath">The root path where folders should exists.</param>
        public static void CreateDirectories(List<Folder> list, string folderPath)
        {
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Folder dir = list[i];
                    if (dir.ShouldCreate)
                    {
                        string dirPath = folderPath + "/" + dir.Path;

                        if (!System.IO.Directory.Exists(dirPath))
                            System.IO.Directory.CreateDirectory(dirPath);
                    }
                }
            }
        }
    }
}