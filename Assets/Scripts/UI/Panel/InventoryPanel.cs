using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : BasePanel
{
    [SerializeField,ReadOnly] UIInventoryScroll inventoryScroll;
    [SerializeField,ReadOnly] UIToggleGroup toggleGroup;

    public override void OnUpdate()
    {
    }

    public override void FirstLoad()
    {
        inventoryScroll.LoadData();
    }

    public override void Open()
    {
        toggleGroup.ResetToggle();
    }

    public override void Close()
    {

    }
}
