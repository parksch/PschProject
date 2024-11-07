using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Text reinforce;
    [SerializeField] Text lv;
    [SerializeField,ReadOnly] BaseItem item;

    public void SetItem(BaseItem _item)
    {
        item = _item;
    }

    public void UpdateItem()
    {
        if (item.ID == "")
        {
            image.gameObject.SetActive(false);
            lv.gameObject.SetActive(false);
            reinforce.gameObject.SetActive(false);
        }
        else
        {
            lv.text = "Lv" + item.Level;
            reinforce.text = $"+{item.Reinforce}";
            image.sprite = item.GetSprite;
            image.gameObject.SetActive(true);
            lv.gameObject.SetActive(true);
            reinforce.gameObject.SetActive(true);
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
