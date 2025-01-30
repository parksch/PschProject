using ClientEnum;
using GoogleMobileAds.Api;
using JsonClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonSweepPanel : BasePanel
{
    [SerializeField] Text titleText;
    [SerializeField,ReadOnly] Text countText;

    DungeonsData target;
    int count;

    bool Check(int check)
    {
        switch (target.NeedGoods())
        {
            case ClientEnum.Goods.GoldDungeonTicket:
                return DataManager.Instance.CheckGoods(ClientEnum.Goods.GoldDungeonTicket, check);
            case ClientEnum.Goods.GemDungeonTicket:
                return DataManager.Instance.CheckGoods(ClientEnum.Goods.GemDungeonTicket, check);
            default:
                break;
        }

        return false;
    }

    int Get()
    {
        switch (target.NeedGoods())
        {
            case ClientEnum.Goods.GoldDungeonTicket:
                return (int)DataManager.Instance.GetGoods(ClientEnum.Goods.GoldDungeonTicket);
            case ClientEnum.Goods.GemDungeonTicket:
                return (int)DataManager.Instance.GetGoods(ClientEnum.Goods.GemDungeonTicket);
            default:
                break;
        }

        return 0;
    }

    public void SetTarget(DungeonsData data)
    {
        target = data;
        titleText.text = target.Title() + " " + ScriptableManager.Instance.Get<LocalizationScriptable>(ScriptableType.Localization).Get("Sweep");
    }

    public override void Open()
    {
        base.Open();
        count = 0;
        countText.text = "0";
    }

    public void OnClickMin()
    {
        if (count > 0)
        {
            count = 0;
            countText.text = "0";
        }
    }

    public void OnClickMax()
    {
        count = Get();
        countText.text = count.ToString();
    }

    public void OnClickPrev()
    {
        if (count > 0)
        {
            count -= 1;
            countText.text = count.ToString();
        }
    }

    public void OnClickNext()
    {
        if (Check(count + 1))
        {
            count += 1;
            countText.text = count.ToString();
        }
    }

    public void OnClickYes()
    {
        if (count == 0)
        {
            CommonPanel commonPanel = UIManager.Instance.Get<CommonPanel>();
            switch (target.GameMode())
            {
                case GameMode.GoldDungeon:
                    if(DataManager.Instance.GetGoods(Goods.GoldDungeonTicket) == 0)
                    {
                        commonPanel.SetOK("NotEnoughGoods");
                        UIManager.Instance.AddPanel(commonPanel);
                    }
                    break;
                case GameMode.GemDungeon:
                    if (DataManager.Instance.GetGoods(Goods.GemDungeonTicket) == 0)
                    {
                        commonPanel.SetOK("NotEnoughGoods");
                        UIManager.Instance.AddPanel(commonPanel);
                    }
                    break;
                default:
                    break;
            }
            return;
        }

        RewardPanel rewardPanel = UIManager.Instance.Get<RewardPanel>();
        int level = 0;
        List<(int goodsIndex, int value)> rewards = null;

        switch (target.GameMode())
        {
            case GameMode.GoldDungeon:
                level = DataManager.Instance.CurrentGoldDungeon;
                DataManager.Instance.UseGoods(Goods.GoldDungeonTicket, count);
                break;
            case GameMode.GemDungeon:
                level = DataManager.Instance.CurrentGemDungeon;
                DataManager.Instance.UseGoods(Goods.GemDungeonTicket, count);
                break;
            default:
                break;
        }

        rewards = target.GetRewards(level);

        for (int i = 0; i < rewards.Count; i++)
        {
            DataManager.Instance.AddGoods((Goods)rewards[i].goodsIndex, rewards[i].value * count);
            rewardPanel.AddGoods((Goods)rewards[i].goodsIndex, rewards[i].value * count);
        }

        UIManager.Instance.AddPanel(rewardPanel);

        OnClickMin();
    }

    public void OnClickNo()
    {
        UIManager.Instance.BackPanel();
    }
}
