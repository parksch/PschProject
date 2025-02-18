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
        public int guideKey;
        public int guideValue;
        public string guideName;
        public int rewardType;
        public int rewardIndex;
        public int rewardValue;
        public string local;
    }

}
