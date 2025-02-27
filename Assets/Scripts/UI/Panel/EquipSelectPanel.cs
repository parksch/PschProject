using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipSelectPanel : BasePanel
{
    [SerializeField] UIItemInfo prev;
    [SerializeField] UIItemInfo select;

    public void SetSelect(BaseItem item)
    {
        if (CheckInventory(item))
        {
            select.gameObject.SetActive(false);
        }
        else
        {
            select.SetItem(item);
            select.gameObject.SetActive(true);
        }

        CheckReinforce();
    }

    bool CheckInventory(BaseItem item)
    {
        bool result = false;

        switch (item.Type)
        {
            case ClientEnum.Item.Helmet:
                if (DataManager.Instance.Helmet.ID == "")
                {
                    prev.gameObject.SetActive(false);
                }
                else
                {
                    prev.SetItem(DataManager.Instance.Helmet);
                    prev.gameObject.SetActive(true);
                    result = item.ID == DataManager.Instance.Helmet.ID;
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
                    result = item.ID == DataManager.Instance.Armor.ID;
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
                    result = item.ID == DataManager.Instance.Weapon.ID;
                }
                break;
            default:
                break;
        }

        return result;
    }

    public void CheckReinforce()
    {
        if (prev.gameObject.activeSelf)
        {
            prev.ReinforceUpdate();
        }

        select.ReinforceUpdate();
    }
}
