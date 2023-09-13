using System.Collections.Generic;
using System;

namespace StartupContentTemplate
{
    /// <summary>
    /// SCT Json root.
    /// </summary>
    [Serializable]
    public class SCT_JSonContainer
    {
        /// <summary>
        /// The directory's list from json list called "Directories".
        /// </summary>
        public List<string> Directories;
    }
}
