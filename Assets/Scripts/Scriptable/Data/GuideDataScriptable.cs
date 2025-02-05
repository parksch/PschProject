using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class GuideDataScriptable : ScriptableObject
    {
        public List<GuideData> guideData = new List<GuideData>();
    }

    [System.Serializable]
    public partial class GuideData
    {
        public int id;
        public int next;
        public int guideType;
        public string guideName;
        public int guideValue;
        public int rewardType;
        public int rewardIndex;
        public int rewardValue;
        public string local;
    }

}
