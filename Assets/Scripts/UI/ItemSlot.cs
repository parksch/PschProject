using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField, ReadOnly] Image image;
    [SerializeField, ReadOnly] Text text;
    [SerializeField, ReadOnly] BaseItem item;

    public void SetItem(BaseItem _item)
    {
        item = _item;

        if (item.ID == "")
        {
            image.gameObject.SetActive(false);
            text.gameObject.SetActive(false);
        }
        else
        {
            image.gameObject.SetActive(true);
            text.gameObject.SetActive(true);
            text.text = "Lv" + item.Level;
            image.sprite = item.GetSprite;
        }
    }

    public void OnClickItemSlot()
    {
        if (item.ID != "")
        {
            UIManager.Instance.GetPanel<InventoryPanel>().OpenItemSelect(item);
        }
    }
}
