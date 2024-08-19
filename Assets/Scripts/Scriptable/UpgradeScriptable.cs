using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade",menuName = "Scriptable/Upgrade")]
public class UpgradeScriptable : BaseScriptable
{
    [SerializeField] List<UpgradeState> upgradeStates = new List<UpgradeState>();

    [System.Serializable]
    public class UpgradeState
    {
        public ClientEnum.UpgradeType upgradeType;
        public Sprite sprite;
        public string upgradeKey;
        public string name;
        public int maxLevel;
        public float addValue;
        public int needGoods;
        public float levelPerGoods;
    }

    public UpgradeState GetUpgradeState(string name) => upgradeStates.Find(x => x.name == name);
    public List<UpgradeState> GetUpgradeType(ClientEnum.UpgradeType upgradeType) => upgradeStates.FindAll(x => x.upgradeType == upgradeType);
}
