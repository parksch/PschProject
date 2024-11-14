using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageResultPanel : BasePanel
{
    [SerializeField,ReadOnly] Text title;
    [SerializeField] RectTransform content;
    [SerializeField] UIRewardSlot rewardSlotPrefab;
    [SerializeField] List<UIRewardSlot> rewards;

    public void AddGoods(ClientEnum.Goods goods,int value)
    {

    }

    public override void Open()
    {
        base.Open();
        content.anchoredPosition = Vector2.zero;
    }

    public override void Close()
    {
        for (int i = 0; i < rewards.Count; i++)
        {
            rewards[i].gameObject.SetActive(false);
        }
    }
}
