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
        public List<int> randomTarget;
        public int target;
        public List<Items> items;
        public float mainStateAddValue;
    }

    [System.Serializable]
    public partial class Items
    {
        public string id;
        public string sprite;
        public string prefab;
        public int mainState;
    }

}
