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
    public void LoadData()
    {
        List<DataManager.InventoryData> data = DataManager.Instance.InventoryDatas;

        for (int i = 0; i < data.Count / slotCount; i++)
        {
            InventoryData cell = new InventoryData();
            cell.index = i + 1;
            cell.datas = new List<DataManager.InventoryData>(new DataManager.InventoryData[slotCount]);

            for (int j = 0; j < slotCount; j++)
            {
                if ((i * slotCount) + j < data.Count)
                {
                    cell.datas[j] = data[(i * slotCount) + j];
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
