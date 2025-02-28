using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class  DataManager //Goods
{
    Dictionary<ClientEnum.Goods, UNBigStats> goodsBigStats = new Dictionary<ClientEnum.Goods, UNBigStats>();
    Dictionary<ClientEnum.Goods, long> goodsDict = new Dictionary<ClientEnum.Goods, long>();

    public delegate void ChangeGoods(ClientEnum.Goods type,long value);

    public ChangeGoods OnChangeGoods;

    public long GetGoods(ClientEnum.Goods type) => goodsDict[type];

    public bool CheckGoods(ClientEnum.Goods type, long need)
    {
        return goodsDict[type] >= need;
    }

    public void UseGoods(ClientEnum.Goods type, long value)
    {
        goodsDict[type] -= value;
        OnChangeGoods(type,goodsDict[type]);
    }

    public void AddGoods(ClientEnum.Goods type, long value)
    {
        goodsDict[type] += value;
        OnChangeGoods(type, goodsDict[type]);
    }

    void InitGoods()
    {
        for (int i = (int)ClientEnum.Goods.Gold; i < (int)ClientEnum.Goods.Max; i++)
        {
            goodsDict[(ClientEnum.Goods)i] = 0;
        }

        goodsDict[ClientEnum.Goods.GoldDungeonTicket] = 10;
        goodsDict[ClientEnum.Goods.GemDungeonTicket] = 10;

        OnChangeGoods = null;

        goodsDict[ClientEnum.Goods.Gold] = 100000;
        goodsDict[ClientEnum.Goods.Gem] = 100000;
        goodsDict[ClientEnum.Goods.Scrap] = 1000;
    }
}
