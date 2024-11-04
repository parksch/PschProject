using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardPanel : BasePanel
{
    [SerializeField] RectTransform content;
    [SerializeField] UIRewardSlot slotPrefab;
    [SerializeField] List<UIRewardSlot> slots = new List<UIRewardSlot>();
    [SerializeField] int count = 0;

    public override void OnUpdate()
    {

    }

    public override void FirstLoad()
    {

    }

    public void AddItem(BaseItem item)
    {
        if (count >= slots.Count)
        {
            UIRewardSlot slot = Instantiate(slotPrefab, content);
            slots.Add(slot);
        }

        slots[count].SetItem(item);
        count++;
    }

    public void AddGoods(ClientEnum.Goods goods,int value)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].gameObject.activeSelf)
            {
               if(slots[i].CheckGoods(goods, value))
                {
                    return;
                }
            }
        }

        if (count >= slots.Count)
        {
            UIRewardSlot slot = Instantiate(slotPrefab, content);
            slots.Add(slot);
        }

        slots[count].SetGoods(goods,value);
        count++;
    }

    public override void Close()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].gameObject.SetActive(false);
        }

        count = 0;

        gameObject.SetActive(false);
    }

    public override void Open()
    {
        gameObject.SetActive(true);
    }

}
