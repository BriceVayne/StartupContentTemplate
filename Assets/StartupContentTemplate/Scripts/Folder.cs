using System;
using System.Collections.Generic;

namespace SCT
{
    [Serializable]
    public class Folder
    {
        public string Name;
        public bool ShouldCreate;
        public List<Folder> Content;
    }
}

