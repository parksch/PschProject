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
        public int mainState;
        public string atlas;
        public List<GradeItem> gradeItems;
    }

    [System.Serializable]
    public partial class GradeItem
    {
        public int grade;
        public float startValue;
        public float mainStateAddValue;
        public List<Items> items;
    }

    [System.Serializable]
    public partial class Items
    {
        public string sprite;
        public string prefab;
        public string local;
    }
}
