using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEquipmentSlot : MonoBehaviour
{
    [SerializeField,ReadOnly] Image icon;
    [SerializeField,ReadOnly] BaseItem target;

    public void Set(BaseItem item)
    {
        target = item;

    }

    public void OnClickButton()
    {
        if (target.Type == ClientEnum.Item.None)
        {
            return;
        }
    }
}
