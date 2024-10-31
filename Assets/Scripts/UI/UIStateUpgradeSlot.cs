using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JsonClass;

public class UIStateUpgradeSlot : MonoBehaviour
{
    [SerializeField, ReadOnly] UIButton button;
    [SerializeField, ReadOnly] Image icon;
    [SerializeField, ReadOnly] Upgrade targetState;
    [SerializeField, ReadOnly] Text desc;
    [SerializeField] Text lv;
    [SerializeField] Text price;

    long GetNeedValue()
    {
        long result = targetState.needGoods;
        int targetLv = DataManager.Instance.GetUpgradeLevel(targetState.name);

        for (int i = 0;  i < targetLv; i++)
        {
            result += Mathf.RoundToInt(result * targetState.levelPerGoods);
        }

        return result;
    }

    public void Init(Upgrade upgradeState)
    {
        targetState = upgradeState;
        desc.text = ScriptableManager.Instance.Get<LocalizationScriptable>(ScriptableType.Localization).Get(targetState.upgradeKey);
        icon.sprite = targetState.GetSprite();
    }

    public void UpdateSlot()
    {
        long need = GetNeedValue();
        lv.text = "Lv" + DataManager.Instance.GetUpgradeLevel(targetState.name);

        if (DataManager.Instance.GetUpgradeLevel(targetState.name) >= targetState.maxLevel)
        {
            button.SetInterractable(false);
            price.text = "Max";
            return;
        }
        else if (!DataManager.Instance.CheckGoods(targetState.Goods(), need))
        {
            button.SetInterractable(false);
        }
        else
        {
            button.SetInterractable(true);
        }

        price.text = need.ToString();
    }

    public void OnClickSlot()
    {

    }
}
