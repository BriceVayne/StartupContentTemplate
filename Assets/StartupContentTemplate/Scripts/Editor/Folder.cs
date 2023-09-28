using System.Linq;
using System;

namespace SCT
{
    /// <summary>
    /// SCT Folder representation.
    /// </summary>
    [Serializable]
    public class Folder
    {
        /// <summary>
        /// Folder name.
        /// </summary>
        public string Name;

        /// <summary>
        /// Should create the folder.
        /// </summary>
        public bool ShouldCreate;

        /// <summary>
        /// Folder path.
        /// </summary>
        public string Path;

        /// <summary>
        /// The depth from the main folder. Used to subfolder.
        /// </summary>
        public int Depth;

        /// <summary>
        /// Setup data from path.
        /// </summary>
        /// <param name="path"></param>
        public Folder(string path)
        {
            string[] splitPath = path.Split('/');

            Name = splitPath.Last();
            ShouldCreate = false;
            Path = path;
            Depth = splitPath.Length;
        }
    }
}