using JsonClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class DataManager //State
{
    public PlayerState PlayerDefaultState => playerDefaultState;
    public int GetUpgradeLevel(string code) => upgradeLevel[code];
    public void AddUpgradeLevel(string code, int num = 1)
    {
        upgradeLevel[code] += num;
        GameManager.Instance.Player.StateUpdate();
    }

    [SerializeField, ReadOnly] PlayerState playerDefaultState;

    Dictionary<string, int> upgradeLevel = new Dictionary<string, int>();
    
    void StateInit()
    {
        List<Upgrade> upgradeStates = ScriptableManager.Instance.Get<UpgradeScriptable>(ScriptableType.Upgrade).GetUpgradeType(ClientEnum.UpgradeType.StatePanel);

        for (int i = 0; i < upgradeStates.Count; i++)
        {
            upgradeLevel[upgradeStates[i].name] = 0;
        }
    }

    public float GetUpgradeValue(ClientEnum.State state,ClientEnum.ChangeType changeType)
    {
        Upgrade upgrade = ScriptableManager.Instance.Get<UpgradeScriptable>(ScriptableType.Upgrade).GetUpgrade(state, changeType);
        
        if (upgrade == null)
        {
            return 0;
        }

        float result = upgrade.GetLevelValue(upgradeLevel[upgrade.name]);
        return result;
    }

}
