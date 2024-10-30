using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JsonClass;

public class UIStateUpgradeSlot : MonoBehaviour
{
    [SerializeField, ReadOnly] Image icon;
    [SerializeField, ReadOnly] Upgrade targetState;
    [SerializeField, ReadOnly] Text desc;
    [SerializeField, ReadOnly] Text lvPrice;

    public void Init(Upgrade upgradeState)
    {
        targetState = upgradeState;
        desc.text = ScriptableManager.Instance.Get<LocalizationScriptable>(ScriptableType.Localization).Get(targetState.upgradeKey);
        //icon.sprite = targetState.sprite;

    }

    public void UpdateSlot()
    {

    }
}
