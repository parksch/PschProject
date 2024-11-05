using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventorySelect : MonoBehaviour
{
    [SerializeField, ReadOnly] UIItemInfo prev;
    [SerializeField, ReadOnly] UIItemInfo select;

    public void OpenSlect(BaseItem item)
    {
        switch (item.Type)
        {
            case ClientEnum.Item.Helmet:
                if (DataManager.Instance.Helmat.ID == "")
                {
                    prev.gameObject.SetActive(false);
                }
                else
                {
                    prev.SetItem(DataManager.Instance.Helmat);
                    prev.gameObject.SetActive(true);
                }
                break;
            case ClientEnum.Item.Armor:
                if (DataManager.Instance.Armor.ID == "")
                {
                    prev.gameObject.SetActive(false);
                }
                else
                {
                    prev.SetItem(DataManager.Instance.Armor);
                    prev.gameObject.SetActive(true);
                }
                break;
            case ClientEnum.Item.Weapon:
                if (DataManager.Instance.Weapon.ID == "")
                {
                    prev.gameObject.SetActive(false);
                }
                else
                {
                    prev.SetItem(DataManager.Instance.Weapon);
                    prev.gameObject.SetActive(true);
                }
                break;
            default:
                break;
        }

        select.SetItem(item);
        gameObject.SetActive(true);
    }

    public void CloseSelect()
    {
        gameObject.SetActive(false);
    }
}
