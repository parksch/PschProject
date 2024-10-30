using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class DrawScriptable : ScriptableObject
    {
        public List<Draw> draw = new List<Draw>();
    }

    [System.Serializable]
    public partial class Draw
    {
        public int index;
        public Type type;
    }

    [System.Serializable]
    public partial class Type
    {
        public string titleKey;
        public List<Shops> shops;
    }

    [System.Serializable]
    public partial class Shops
    {
        public List<Probabilities> probabilities;
        public int target;
        public int goods;
        public string descKey;
        public string nameKey;
        public int needValue;
        public int limit;
        public int maxLevel;
    }

    [System.Serializable]
    public partial class Probabilities
    {
        public int grade;
        public int value;
    }

}
