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
        public int type;
        public int maxLevel;
        public int needGoodsType;
        public int itemType;
        public int startValue;
        public float itemAddValueType;
        public float addValue;
        public string nameLocal;
        public string descriptionLocal;
        public string mapPrefab;
        public List<string> monsters;
    }
}
