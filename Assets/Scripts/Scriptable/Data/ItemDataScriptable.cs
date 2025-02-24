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
        public List<GradeItems> gradeItems;
        public List<int> options;
    }

    [System.Serializable]
    public partial class GradeItems
    {
        public int grade;
        public int startValue;
        public float mainStateAddValue;
        public List<ResourcesItems> resourcesItems;
    }

    [System.Serializable]
    public partial class ResourcesItems
    {
        public string sprite;
        public string prefab;
        public string local;
    }

}
