using ClientEnum;
using JsonClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class DataManager //State
{
    public PlayerState PlayerDefaultState => playerDefaultState;
    public int GetUpgradeLevel(string code) => data[code].level;
    public void OnChangeLevelUp(string code, int num = 1)
    {
        data[code].level += num;
        changeLevelUp(code);
    }

    [SerializeField, ReadOnly] PlayerState playerDefaultState;
    
    delegate void ChangeLevelUp(string code);

    ChangeLevelUp changeLevelUp;
    Dictionary<string, UpgradeState> data = new Dictionary<string, UpgradeState>();

    class UpgradeState
    {
        public int level = 0;
        public float velue = 0;
    }
    
    void StateInit()
    {
        List<Upgrade> upgradeStates = ScriptableManager.Instance.Get<UpgradeScriptable>(ScriptableType.Upgrade).GetUpgradeType(ClientEnum.UpgradeType.StatePanel);

        for (int i = 0; i < upgradeStates.Count; i++)
        {
            data[upgradeStates[i].name] = new UpgradeState();
        }

        changeLevelUp = (code)=> 
        {
            Upgrade upgrade = ScriptableManager.Instance.Get<UpgradeScriptable>(ScriptableType.Upgrade).GetUpgradeState(code);
            data[code].velue = upgrade.GetLevelValue(data[code].level);
            GameManager.Instance.Player.StateUpdate();
        };
    }

    float GetUpgradeValue(ClientEnum.State state,ClientEnum.ChangeType changeType)
    {
        Upgrade upgrade = ScriptableManager.Instance.Get<UpgradeScriptable>(ScriptableType.Upgrade).GetUpgrade(state,changeType);

        if (upgrade == null)
        {
            return 0;
        }

        return data[upgrade.name].velue;
    }
}
