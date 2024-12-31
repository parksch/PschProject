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

    bool Check()
    {
        switch (target.NeedGoods())
        {
            case ClientEnum.Goods.GoldDungeonTicket:
                return DataManager.Instance.CheckGoods(ClientEnum.Goods.GoldDungeonTicket, count);
            case ClientEnum.Goods.GemDungeonTicket:
                return DataManager.Instance.CheckGoods(ClientEnum.Goods.GemDungeonTicket, count);
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
        if (Check())
        {
            count += 1;
            countText.text = count.ToString();
        }
    }

    public void OnClickYes()
    {

    }

    public void OnClickNo()
    {

    }
}
