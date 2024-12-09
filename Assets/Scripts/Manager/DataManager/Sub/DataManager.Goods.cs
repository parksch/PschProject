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
    public ChangeGoods OnChangeReinforce;
    public ChangeGoods OnChangeAmplification;

    [System.Serializable]
    public class Goods
    {
        public long gold = 0;
        public long gem = 0;
        public long scrap = 0;
        public long amplification = 0;
        public long reinforce = 0;
    }

    public void AddGem(long value)
    {
        goods.gem += value;
    }

    public void AddScrap(long value)
    {
        goods.scrap += value;
    }

    public void AddGold(long value)
    {
        goods.gold += value;
    }

    public void AddReinforce(long value)
    {
        goods.reinforce += value;
    }

    public void AddAmplification(long value)
    {
        goods.amplification += value;
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
            case ClientEnum.Goods.Reinforce:
                return goods.reinforce >= need;
            case ClientEnum.Goods.Amplification:
                return goods.amplification >= need;
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
                OnChangeScrap(-value);
                break;
            case ClientEnum.Goods.Gold:
                OnChangeGold(-value);
                break;
            case ClientEnum.Goods.Gem:
                OnChangeGem(-value);
                break;
            case ClientEnum.Goods.Money:
                break;
            case ClientEnum.Goods.Reinforce:
                OnChangeReinforce(-value);
                break;
            case ClientEnum.Goods.Amplification:
                OnChangeAmplification(-value);
                break;
            default:
                break;
        }
    }

    public void AddGoods(ClientEnum.Goods type, long value)
    {
        switch (type)
        {
            case ClientEnum.Goods.Scrap:
                OnChangeScrap(value);
                break;
            case ClientEnum.Goods.Gold:
                OnChangeGold(value);
                break;
            case ClientEnum.Goods.Gem:
                OnChangeGem(value);
                break;
            case ClientEnum.Goods.Money:
                break;
            case ClientEnum.Goods.Reinforce:
                OnChangeReinforce(value);
                break;
            case ClientEnum.Goods.Amplification:
                OnChangeAmplification(value);
                break;
            default:
                break;
        }
    }

    void InitGoods()
    {
        OnChangeGem = null;
        OnChangeGem += AddGem;

        OnChangeScrap = null;
        OnChangeScrap += AddScrap;

        OnChangeGold = null;
        OnChangeGold += AddGold;

        OnChangeReinforce = null;
        OnChangeReinforce += AddReinforce;

        OnChangeAmplification = null;
        OnChangeAmplification += AddAmplification;
    }
}
