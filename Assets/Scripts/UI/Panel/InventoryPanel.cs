using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : BasePanel
{
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
        inventoryScroll.LoadData(target);
    }

    public override void Open()
    {
        toggleGroup.ResetToggle();
        inventoryScroll.LoadData(target);
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
}
