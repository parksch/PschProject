using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class ContentLockScriptable : ScriptableObject
    {
        public List<ContentLock> contentLock = new List<ContentLock>();
    }

    [System.Serializable]
    public partial class ContentLock
    {
        public int id;
        public int contentLockType;
        public int targetValue;
        public string openLocal;
        public string clicklocal;
    }

}
