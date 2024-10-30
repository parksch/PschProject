using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class  DataManager //Goods
{
    [SerializeField] Goods goods;

    public Goods GetGoods => goods;

    public delegate void ChangeGoods(long value);

    public ChangeGoods OnChangeGem;
    public ChangeGoods OnChangeScrap;
    public ChangeGoods OnChangeGold;
    
    [System.Serializable]
    public class Goods
    {
        public long gold = 0;
        public long gem = 0;
        public long scrap = 0;
    }

    public void AddScrap(long value)
    {
        goods.scrap += value;

        OnChangeScrap(goods.scrap);
    }

    public void AddGold(long value)
    {
        goods.gold += value;

        OnChangeGold(goods.gold);
    }

    public void AddGem(long value)
    {
        goods.gem += value;

        OnChangeGem(goods.gem);
    }

    public bool CheckGoods(ClientEnum.Goods type, long need)
    {
        switch (type)
        {
            case ClientEnum.Goods.Scrap:
                return goods.scrap >= need;
            case ClientEnum.Goods.Gold:
                return goods.gold >= need;
            case ClientEnum.Goods.Gem:
                return goods.gem >= need;
            default:
                break;
        }

        return false;
    }

    public void UseGoods(ClientEnum.Goods type, long value)
    {
        switch (type)
        {
            case ClientEnum.Goods.Scrap:
                AddScrap(-value);
                break;
            case ClientEnum.Goods.Gold:
                AddGold(-value);
                break;
            case ClientEnum.Goods.Gem:
                AddGem(-value);
                break;
            case ClientEnum.Goods.Money:
                break;
            default:
                break;
        }
    }

}
