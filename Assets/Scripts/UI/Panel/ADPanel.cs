using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADPanel : BasePanel
{
    public override void FirstLoad()
    {
        SDKManager.Instance.ResetAction();
        SDKManager.Instance.AddAdFullScreenContentClosed(UIManager.Instance.BackPanel);
        SDKManager.Instance.AddAdFullScreenContentClosed(() => 
        {
            RewardPanel rewardPanel = UIManager.Instance.Get<RewardPanel>();

            DataManager.Instance.AddGoods(ClientEnum.Goods.Gold, 100000);
            DataManager.Instance.AddGoods(ClientEnum.Goods.Gem, 1000);

            rewardPanel.AddGoods(ClientEnum.Goods.Gold, 100000);
            rewardPanel.AddGoods(ClientEnum.Goods.Gem, 1000);

            UIManager.Instance.AddPanel(rewardPanel);
        });
        SDKManager.Instance.AddAdFullScreenContentFailed(UIManager.Instance.BackPanel);
        SDKManager.Instance.AddAdFullScreenContentFailed(() => 
        {
            CommonPanel commonPanel = UIManager.Instance.Get<CommonPanel>();
            commonPanel.SetOK("광고를 불러오는데 실패 했습니다.");
        });
    }

    public override void Open()
    {
        base.Open();

        SDKManager.Instance.ShowRewardedAD();
    }

    void AdSuccess()
    {

    }

    void AdFail()
    {

    }
}
