using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class UpgradeScriptable // This Class is a functional Class.
    {
        [System.Serializable]
        public class UpgradeState
        {
            public ClientEnum.UpgradeType upgradeType;
            public string sprite;
            public string upgradeKey;
            public string name;
            public int maxLevel;
            public float addValue;
            public int needGoods;
            public float levelPerGoods;
        }

        public List<UpgradeState> upgradeStates = new List<UpgradeState>();
        public UpgradeState GetUpgradeState(string name) => upgradeStates.Find(x => x.name == name);
        public List<UpgradeState> GetUpgradeType(ClientEnum.UpgradeType upgradeType) => upgradeStates.FindAll(x => x.upgradeType == upgradeType);
    }

    public partial class Upgrade
    {
    }

}
