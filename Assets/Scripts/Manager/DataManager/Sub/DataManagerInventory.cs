using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class DataManager //Inventory
{
    [SerializeField] BaseItem equipHelmat;
    [SerializeField] BaseItem equipWeapon;
    [SerializeField] BaseItem equipArmor;

    [SerializeField, ReadOnly] List<BaseItem> inventoryDatas = new List<BaseItem>();

    public BaseItem Helmat => equipHelmat;
    public BaseItem Weapon => equipWeapon;
    public BaseItem Armor => equipArmor;

    public List<BaseItem> InventoryDatas => inventoryDatas;

    public void AddItem(BaseItem item)
    {
        for (int i = 0; i < inventoryDatas.Count; i++)
        {
            if (inventoryDatas[i].ID == "")
            {
                inventoryDatas[i] = item;
                break;
            }
        }

    }

    public void EquipItem(BaseItem item)
    {
        switch (item.Type)
        {
            case ClientEnum.Item.Helmet:
                equipHelmat = item;
                break;
            case ClientEnum.Item.Armor:
                equipArmor = item;
                break;
            case ClientEnum.Item.Weapon:
                equipWeapon = item;
                break;
            default:
                break;
        }
    }

}
