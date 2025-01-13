using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class UIInventoryScroll : UIRecycleViewController<InventoryData>
{
    [SerializeField] RectTransform content;
    [SerializeField] int slotCount = 5;

    public void LoadData(ClientEnum.Item item)
    {
        int count = DataManager.Instance.InventoryDates.Count;
        List<BaseItem> data = null;

        if (item != ClientEnum.Item.None)
        {
            data = DataManager.Instance.InventoryDates.FindAll(x => x.Type == item);
        }
        else
        {
            data = DataManager.Instance.InventoryDates;
        }

        tableData.Clear();

        for (int i = 0; i < count / slotCount; i++)
        {
            InventoryData cell = new InventoryData();
            cell.index = i + 1;
            cell.datas = new List<BaseItem>(new BaseItem[slotCount]);

            for (int j = 0; j < slotCount; j++)
            {
                if ((i * slotCount) + j < data.Count)
                {
                    cell.datas[j] = data[(i * slotCount) + j];
                }
                else
                {
                    cell.datas[j] = new BaseItem();
                }
            }

            tableData.Add(cell);
        }

        InitializeTableView();
    }

    protected override void Start()
    {
        base.Start();
    }

}
