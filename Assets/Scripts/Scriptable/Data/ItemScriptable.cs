using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class ItemScriptable : ScriptableObject
    {
        public List<Item> item = new List<Item>();
    }

    [System.Serializable]
    public partial class Item
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
