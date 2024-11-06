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
                if (equipHelmat.ID != "")
                {
                    equipHelmat.Disassembly();
                }
                break;
            case ClientEnum.Item.Armor:
                if (equipArmor.ID != "")
                {
                    equipArmor.Disassembly();
                }
                break;
            case ClientEnum.Item.Weapon:
                if (equipWeapon.ID != "")
                {
                    equipWeapon.Disassembly();
                }
                break;
            default:
                break;
        }

        equipItem(item);
    }
    
    delegate void OnEquipItem(BaseItem item);
    OnEquipItem equipItem;

    void InventoryInit()
    {
        equipItem = (item) =>
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

            GameManager.Instance.Player.StateUpdate();
        };
    }

    float GetItemValue(ClientEnum.State state,ClientEnum.ChangeType type)
    {
        if (type == ClientEnum.ChangeType.Sum)
        {
            return 0;
        }

        float value = 0;

        if (equipHelmat.ID != "")
        {
            value += equipHelmat.GetStateValue(state);
        }

        if (equipArmor.ID != "")
        {
            value += equipArmor.GetStateValue(state);
        }

        if (equipWeapon.ID != "")
        {
            value += equipWeapon.GetStateValue(state);
        }

        return value;
    }
}
