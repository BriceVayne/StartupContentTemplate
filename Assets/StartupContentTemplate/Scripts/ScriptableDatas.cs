using System.Collections.Generic;
using UnityEngine;

namespace SCT
{
    [CreateAssetMenu(fileName = "FolderDatas", menuName = "SCT/FolderData", order = 1)]
    public class ScriptableDatas : ScriptableObject
    {
        public List<string> folders;
    }
}
