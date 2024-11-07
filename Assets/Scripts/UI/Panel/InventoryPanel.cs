using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : BasePanel
{
    [SerializeField, ReadOnly] ItemSlot helmet;
    [SerializeField, ReadOnly] ItemSlot armor;
    [SerializeField, ReadOnly] ItemSlot weapon;

    [SerializeField, ReadOnly] UIInventorySelect select;
    [SerializeField, ReadOnly] UIInventoryScroll inventoryScroll;
    [SerializeField, ReadOnly] UIToggleGroup toggleGroup;
    [SerializeField, ReadOnly] ClientEnum.Item target;

    public override void OnUpdate()
    {

    }

    public override void FirstLoad()
    {
        target = ClientEnum.Item.None;
    }

    public override void Open()
    {
        toggleGroup.ResetToggle();
        UpdatePanel();
    }

    public override void Close()
    {

    }

    public void OnToggle(int value)
    {
        target = (ClientEnum.Item)value;
    }

    public void OpenItemSelect(BaseItem item)
    {
        select.OpenSlect(item);
    }

    public void UpdatePanel()
    {
        helmet.SetItem(DataManager.Instance.Helmet);
        helmet.UpdateItem();
        armor.SetItem(DataManager.Instance.Armor);
        armor.UpdateItem();
        weapon.SetItem(DataManager.Instance.Weapon);
        weapon.UpdateItem();
        inventoryScroll.LoadData(target);
    }
}
