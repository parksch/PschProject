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
        public int next;
        public int guideType;
        public string guideName;
        public int guideValue;
        public int rewardType;
        public int rewardIndex;
        public int value;
        public string local;
    }

}
