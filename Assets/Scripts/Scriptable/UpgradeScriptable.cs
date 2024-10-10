using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class UpgradeScriptable : ScriptableObject
    {
        public List<Upgrade> upgrade;
    }

    [System.Serializable]
    public partial class Upgrade
    {
        public int upgradeType;
        public string sprite;
        public string upgradeKey;
        public string name;
        public int maxLevel;
        public float addValue;
        public int needGoods;
        public float levelPerGoods;
    }

}
