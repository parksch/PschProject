using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItemInfo : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] Text itemName;
    [SerializeField] Text mainState;
    [SerializeField] Text option;

    BaseItem target;

    public void SetItem(BaseItem item)
    {
        target = item;
        icon.sprite = target.GetSprite;
        itemName.text = target.Name;
    }
}
