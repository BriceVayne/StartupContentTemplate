using Codice.CM.Common;
using System;
using System.Xml.Linq;
using TMPro;
using UnityEditor.IMGUI.Controls;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace SCT
{
    /// <summary>
    /// SCT Function used to make the editor tool.
    /// </summary>
    public static class SCT_Library
    {
        public static EventCallback<ChangeEvent<ScriptableDatas>> EventCallback = null;

        public static T Create<T>(string name, StyleSheet style) where T : VisualElement, new()
        {
            var element = new T() { name = name };
            element.styleSheets.Add(style);
            return element;
        }

        public static TitleElement Create(string title, string name, StyleSheet style)
        {
            var element = Create<TitleElement>(name, style);
            element.text = title;
            return element;
        }

        public static Button CreateButton(string title, string name, StyleSheet style)
        {
            var element = Create<Button>(name, style);
            element.text = title;
            return element;
        }

        public static Toggle CreateToggle(string title, string name, StyleSheet style)
        {
            var element = Create<Toggle>(name, style);
            element.label = title;
            return element;
        }

        public static ObjectField Create(Type objectType, string label, string name, StyleSheet style)
        {
            var element = Create<ObjectField>(name, style);
            element.label = label;
            element.objectType = objectType;
            return element;
        }

        public static MultiColumnTreeView Create(Columns columns, string name, StyleSheet style)
        {
            var element = new MultiColumnTreeView(columns);
            element.name = name;
            element.styleSheets.Add(style);
            return element;
        }

        /// <summary>
        /// Create directories.
        /// </summary>
        /// <param name="list">A directory's list.</param>
        /// <param name="folderPath">The root path where folders should exists.</param>
        //public static void CreateDirectories(List<Folder> list, string folderPath)
        //{
        //    if (list != null && list.Count > 0)
        //    {
        //        for (int i = 0; i < list.Count; i++)
        //        {
        //            Folder dir = list[i];
        //            if (dir.ShouldCreate)
        //            {
        //                string dirPath = folderPath + "/" + dir.Path;

        //                if (!System.IO.Directory.Exists(dirPath))
        //                    System.IO.Directory.CreateDirectory(dirPath);
        //            }
        //        }
        //    }
        //}
    }
}