using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JsonClass;

public class UIStateUpgradeSlot : MonoBehaviour
{
    [SerializeField, ReadOnly] UIButton button;
    [SerializeField, ReadOnly] Upgrade targetState;
    [SerializeField, ReadOnly] UIGuideUpdater updater;
    [SerializeField, ReadOnly] Image icon;
    [SerializeField, ReadOnly] Image goodsIcon;
    [SerializeField, ReadOnly] Text desc;
    [SerializeField, ReadOnly] UIText value;
    [SerializeField, ReadOnly] UIText lv;
    [SerializeField, ReadOnly] UIText price;

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
        desc.text = ScriptableManager.Instance.Get<LocalizationScriptable>(ScriptableType.Localization).Get(targetState.localKey);
        icon.sprite = targetState.GetSprite();
        goodsIcon.sprite = ResourcesManager.Instance.GetGoodsSprite(targetState.Goods());
        value.SetText(targetState.GetLevelValue(DataManager.Instance.GetUpgradeLevel(targetState.name)));
        updater.SetCode(targetState.name);
    }

    public void UpdateSlot()
    {
        long need = GetNeedValue();
        lv.SetText("Lv", DataManager.Instance.GetUpgradeLevel(targetState.name));

        if (DataManager.Instance.GetUpgradeLevel(targetState.name) >= targetState.maxLevel)
        {
            button.SetInterractable(false);
            price.SetText("Max");
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

        price.SetText(need);
    }

    public void OnClickSlot()
    {
        DataManager.Instance.UseGoods(targetState.Goods(), GetNeedValue());
        DataManager.Instance.OnChangeLevelUp(targetState.name);
        value.SetText(targetState.GetLevelValue(DataManager.Instance.GetUpgradeLevel(targetState.name)));
        UpdateSlot();
    }
}
