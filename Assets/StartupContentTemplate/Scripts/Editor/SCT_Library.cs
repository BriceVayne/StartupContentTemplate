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