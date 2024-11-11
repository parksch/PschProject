using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : BasePanel
{
    [SerializeField] List<UIEquipmentSlot> slots = new List<UIEquipmentSlot>();
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
        select.CopyTopMenu(activeTopUI);
    }

    public override void Open()
    {
        base.Open();
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
        select.SetSelect(item);
        UIManager.Instance.AddPanel(select);
    }

    public void UpdatePanel()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].SetItem(DataManager.Instance.GetEquipItem(slots[i].ItemType));
            slots[i].UpdateItem();
        }

        inventoryScroll.LoadData(target);
    }
}
