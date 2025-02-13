using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class DataManager //Inventory
{
    [SerializeField, ReadOnly] int maxInventoryCount = 100;
    [SerializeField, ReadOnly] BaseItem equipHelmet;
    [SerializeField, ReadOnly] BaseItem equipWeapon;
    [SerializeField, ReadOnly] BaseItem equipArmor;
    [SerializeField, ReadOnly] List<BaseItem> inventoryDates = new List<BaseItem>();

    public delegate void EquipItem(BaseItem item);
    public EquipItem OnEquipItem;

    public BaseItem Helmet => equipHelmet;
    public BaseItem Weapon => equipWeapon;
    public BaseItem Armor => equipArmor;
    public List<BaseItem> InventoryDates => inventoryDates;
    public BaseItem GetEquipItem(ClientEnum.Item item)
    {
        switch (item)
        {
            case ClientEnum.Item.Helmet:
                return Helmet;
            case ClientEnum.Item.Armor:
                return Armor;
            case ClientEnum.Item.Weapon:
                return Weapon;
            default:
                return null;
        }
    }

    public void AddItem(BaseItem item)
    {
        for (int i = 0; i < inventoryDates.Count; i++)
        {
            if (inventoryDates[i].ID == "")
            {
                inventoryDates[i] = item;
                break;
            }
        }
    }

    public void SetEquipItem(BaseItem item)
    {
        BaseItem target = item;
        inventoryDates.Remove(item);
        inventoryDates.Add(new BaseItem());
        OnEquipItem(target);
    }

    public int GetInventoryEmpty()
    {
        return inventoryDates.FindAll(x => x.ID == "").Count;
    }

    void InventoryInit()
    {
        inventoryDates = new List<BaseItem>();

        for (int i = 0; i < maxInventoryCount; i++)
        {
            inventoryDates.Add(new BaseItem());
        }

        OnEquipItem = (item) =>
        {
            switch (item.Type)
            {
                case ClientEnum.Item.Helmet:
                    if (equipHelmet.ID != "")
                    {
                        equipHelmet.Disassembly();
                    }
                    equipHelmet = item;
                    break;
                case ClientEnum.Item.Armor:
                    if (equipArmor.ID != "")
                    {
                        equipArmor.Disassembly();
                    }
                    equipArmor = item;
                    break;
                case ClientEnum.Item.Weapon:
                    if (equipWeapon.ID != "")
                    {
                        equipWeapon.Disassembly();
                    }
                    equipWeapon = item;
                    break;
                default:
                    break;
            }

            GameManager.Instance.Player.StateUpdate();
        };
    }

    float GetItemValue(ClientEnum.State state, ClientEnum.ChangeType type)
    {
        float value = 0;

        if (equipHelmet.ID != "")
        {
            value += equipHelmet.GetStateValue(state, type);
        }

        if (equipArmor.ID != "")
        {
            value += equipArmor.GetStateValue(state, type);
        }

        if (equipWeapon.ID != "")
        {
            value += equipWeapon.GetStateValue(state, type);
        }

        return value;
    }

    public void SalvageItems(List<ClientEnum.Grade> grades)
    {

    }
}
