using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class ItemDataScriptable : ScriptableObject
    {
        public List<ItemData> itemData = new List<ItemData>();
    }

    [System.Serializable]
    public partial class ItemData
    {
        public int itemType;
        public int grade;
        public int mainState;
        public float mainStateAddValue;
        public string atlas;
        public List<Items> items;
        //public List<int> randomTarget;
        //public int target;
    }

    [System.Serializable]
    public partial class Items
    {
        public string sprite;
        public string prefab;
        public string local;
    }

}
