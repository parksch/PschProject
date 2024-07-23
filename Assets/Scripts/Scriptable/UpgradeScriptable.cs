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
        public string name;
        public int maxLevel;
        public float addValue;
    }

    public UpgradeState GetUpgradeState(string name) => upgradeStates.Find(x => x.name == name);
}
