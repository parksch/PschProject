using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIManager //Reward
{
    [SerializeField, ReadOnly] RewardPanel rewardPanel;

    public void OpenRewardPanel()
    {
        rewardPanel.Open();
    }

    public void AddReward(BaseItem item)
    {
        rewardPanel.AddItem(item);
    }

    public void AddReward(ClientEnum.Goods goods, int value)
    {
        rewardPanel.AddGoods(goods, value);
    }
}
