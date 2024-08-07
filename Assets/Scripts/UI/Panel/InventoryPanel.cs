using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : BasePanel
{
    [SerializeField] UIInventoryScroll inventoryScroll;

    public override void FirstLoad()
    {
        inventoryScroll.LoadData();
    }

    public override void Open()
    {

    }

    public override void Close()
    {

    }
}
