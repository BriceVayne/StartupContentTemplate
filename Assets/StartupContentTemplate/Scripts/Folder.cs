using System;
using System.Collections.Generic;

namespace SCT
{
    [Serializable]
    public class Folder
    {
        public bool ShouldCreate { get; set; }
        public string Path { get; set; }
        public int Id { get; set; }
        public int ParentId { get; set; }

        public string Name;
        public List<Folder> Content;
    }

    [Serializable]
    public class GroupFolder
    {

    }
}
