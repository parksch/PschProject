using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JsonClass;

public class UIStateUpgradeSlot : MonoBehaviour
{
    [SerializeField, ReadOnly] Image icon;
    [SerializeField, ReadOnly] UpgradeScriptable.UpgradeState targetState;
    [SerializeField, ReadOnly] Text desc;
    [SerializeField, ReadOnly] Text lvPrice;

    public void Init(UpgradeScriptable.UpgradeState upgradeState)
    {
        targetState = upgradeState;
        desc.text = ScriptableManager.Instance.LocalizationScriptable.Get(targetState.upgradeKey);
        //icon.sprite = targetState.sprite;

    }

    public void UpdateSlot()
    {

    }
}
