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
        public string map;
        public List<string> monsters;
    }

}
