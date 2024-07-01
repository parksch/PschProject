using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [SerializeField, ReadOnly] string deviceNum;
    [SerializeField, ReadOnly] PlayerState playerDefaultState;
    [SerializeField] Goods goods;
    [SerializeField] UpgradeLevel upgradeLevel;
    [SerializeField] Info info;

    public Goods GetGoods => goods;
    public UpgradeLevel GetUpgradeLevel => upgradeLevel;
    public Info GetInfo => info;

    public delegate void ChangeGoods(long value,ClientEnum.Goods goods);
    public ChangeGoods OnChangeGoods;

    #region PlayerStatus
    public long HP => playerDefaultState.HP;
    public long DeFense => playerDefaultState.Defense;
    public float AttackSpeed => playerDefaultState.AttackSpeed;
    public float MoveSpeed => playerDefaultState.MoveSpeed;
    public float AttackRange => playerDefaultState.AttackRange;
    #endregion

    #region Datas

    [System.Serializable]
    public class Goods
    {
        public long gold = 0;
        public long ruby = 0;
    }

    [System.Serializable]
    public class UpgradeLevel 
    {
        public int attack = 0;
        public int defense = 0;
        public int hpRegen = 0;
    }

    [System.Serializable]
    public class Info
    {
        public string userName;
        public int stage = 0;
        public int currentLevel;
        public int maxExp;
    }

    #endregion

    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    public void Init()
    {
        deviceNum = SystemInfo.deviceUniqueIdentifier;

        OnChangeGoods = (value,type) => 
        {
            switch (type)
            {
                case ClientEnum.Goods.Gold:
                    goods.gold += value;
                    UIManager.Instance.SetGold(goods.gold);
                    break;
                case ClientEnum.Goods.Ruby:
                    goods.ruby += value;
                    UIManager.Instance.SetRuby(goods.ruby);
                    break;
                default:
                    break;
            }
        };
    }
}
