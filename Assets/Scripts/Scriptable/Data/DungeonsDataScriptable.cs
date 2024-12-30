using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class DungeonsDataScriptable : ScriptableObject
    {
        public List<DungeonsData> dungeonsData = new List<DungeonsData>();
    }

    [System.Serializable]
    public partial class DungeonsData
    {
        public int maxLevel;
        public int needGoodsType;
        public int changeType;
        public int startValue;
        public int itemType;
        public List<int> itemIndex;
        public float addValue;
        public string atlas;
        public string sprite;
        public string nameLocal;
        public string descriptionLocal;
        public string mapPrefab;
        public List<string> monsters;
    }
}
