using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class StageDataScriptable : ScriptableObject
    {
        public List<StageData> stageData = new List<StageData>();
    }

    [System.Serializable]
    public partial class StageData
    {
        public int index;
        public int next;
        public string map;
        public string nameKey;
        public string boss;
        public List<string> monsters;
        public List<StageRewards> stageRewards;
    }

    [System.Serializable]
    public partial class StageRewards
    {
        public int goodsIndex;
        public int value;
    }

}
