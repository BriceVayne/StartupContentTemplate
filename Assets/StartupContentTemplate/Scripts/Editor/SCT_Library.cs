using SCT;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace SCT
{
    /// <summary>
    /// SCT Function used to make the editor tool.
    /// </summary>
    public static class SCT_Library
    {
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