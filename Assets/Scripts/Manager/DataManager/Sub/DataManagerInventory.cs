using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class DataManager //Inventory
{
    [SerializeField, ReadOnly] List<BaseItem> inventoryDatas = new List<BaseItem>();
    public List<BaseItem> InventoryDatas => inventoryDatas;

    public void AddItem(BaseItem item)
    {
        inventoryDatas.Add(item);
    }

}
