using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JsonClass;

public class StatePanel : BasePanel
{
    [SerializeField,ReadOnly] List<UIStateUpgradeSlot> stateUpgradeSlots = new List<UIStateUpgradeSlot>();
    [SerializeField,ReadOnly] UIStateUpgradeSlot prefab;

    public override void OnUpdate()
    {
    }

    public override void FirstLoad()
    {
        List<UpgradeScriptable.UpgradeState> upgradeStates;
        upgradeStates = ScriptableManager.Instance.UpgradeScriptable.GetUpgradeType(ClientEnum.UpgradeType.StatePanel);

        for (int i = 0; i < upgradeStates.Count; i++)
        {
            if (i == 0)
            {
                prefab.Init(upgradeStates[i]);
                stateUpgradeSlots.Add(prefab);
            }
            else
            {
                UIStateUpgradeSlot slot = Instantiate(prefab, prefab.transform.parent).GetComponent<UIStateUpgradeSlot>();
                slot.Init(upgradeStates[i]);
                stateUpgradeSlots.Add(slot);
            }
        }
    }

    public override void Open()
    {
    }

    public override void Close()
    {
    }

}
