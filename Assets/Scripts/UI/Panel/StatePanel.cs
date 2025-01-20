using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JsonClass;
using ClientEnum;

public class StatePanel : BasePanel
{
    [SerializeField,ReadOnly] List<UIStateUpgradeSlot> stateUpgradeSlots = new List<UIStateUpgradeSlot>();
    [SerializeField,ReadOnly] UIStateUpgradeSlot prefab;
    [SerializeField,ReadOnly] Text stateText;
    [SerializeField,ReadOnly] Text valueText;

    public override void OnUpdate()
    {
        for (int i = 0; i < stateUpgradeSlots.Count; i++)
        {
            stateUpgradeSlots[i].UpdateSlot();
        }
    }

    public override void FirstLoad()
    {
        List<Upgrade> upgradeStates;
        upgradeStates = ScriptableManager.Instance.Get<UpgradeScriptable>(ScriptableType.Upgrade).GetUpgradeType(ClientEnum.UpgradeType.StatePanel);

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

        UpdateState();
    }

    public override void Open()
    {
        base.Open();

        for (int i = 0; i < stateUpgradeSlots.Count; i++)
        {
            stateUpgradeSlots[i].UpdateSlot();
        }
    }

    public override void Close()
    {

    }

    public void UpdateState()
    {
        string state = string.Empty;
        string value = string.Empty;
        LocalizationScriptable localization = ScriptableManager.Instance.Get<LocalizationScriptable>(ScriptableType.Localization);
        PlayerCharacter player = GameManager.Instance.Player;
        
        for (int i = (int)State.HP; i < (int)State.Max;i++)
        {
            State current = (State)i;
            string local = localization.Get(EnumString<State>.ToString(current));
            string stateValue = string.Empty;

            if (i < (int)State.HpRegen)
            {
                stateValue = player.GetBigState(current).Text;
            }
            else
            {
                stateValue = player.GetState(current).ToString();
            }

            value += $"{local}\n";
            state += $"{stateValue}\n";
        }

        stateText.text = value;
        valueText.text = state;
    }
}
