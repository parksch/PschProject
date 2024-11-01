using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class UpgradeScriptable : ScriptableObject
    {
        public List<Upgrade> upgrade = new List<Upgrade>();
    }

    [System.Serializable]
    public partial class Upgrade
    {
        public int upgradeType;
        public string atlas;
        public string sprite;
        public string upgradeKey;
        public string name;
        public int upgradeState;
        public int changeType;
        public int maxLevel;
        public float addValue;
        public int goodsType;
        public int needGoods;
        public float levelPerGoods;
    }

}
