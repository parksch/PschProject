using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIStateUpgradeSlot : MonoBehaviour
{
    [SerializeField, ReadOnly] Image icon;
    [SerializeField, ReadOnly] UpgradeScriptable.UpgradeState targetState;
    [SerializeField, ReadOnly] Text desc;
    [SerializeField, ReadOnly] Text lvPrice;

    public void Init(UpgradeScriptable.UpgradeState upgradeState)
    {
        targetState = upgradeState;
        desc.text = TableManager.Instance.TextScriptable.Get(targetState.upgradeKey);
        icon.sprite = targetState.sprite;

    }

    public void UpdateSlot()
    {

    }
}
