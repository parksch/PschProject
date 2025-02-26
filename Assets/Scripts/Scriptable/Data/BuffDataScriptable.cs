using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class BuffDataScriptable : ScriptableObject
    {
        public List<BuffData> buffData = new List<BuffData>();
    }

    [System.Serializable]
    public partial class BuffData
    {
        public string name;
        public string prefab;
        public string atlas;
        public string sprite;
        public int state;
        public int isDebuff;
    }

}
