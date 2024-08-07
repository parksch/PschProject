using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InventoryData
{
    public int index = 0;
    public List<DataManager.InventoryData> datas = new List<DataManager.InventoryData>();
}

public class UIInventoryCell : UIRecycleViewCell<InventoryData>
{
    public List<ItemSlot> itemSlots;

    public override void UpdateContent(InventoryData itemData)
    {

    }

}
