using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [SerializeField] Goods goods;
    [SerializeField] UpgradeLevel upgradeLevel;
    [SerializeField] Info info;

    public Goods GetGoods => goods;
    public UpgradeLevel GetUpgradeLevel => upgradeLevel;
    public Info GetInfo => info;

#region Datas

    [System.Serializable]
    public class Goods
    {
        public long gold = 0;
        public long diamond = 0;
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
        public int stage = 0;
    }

    #endregion

    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    public void Init()
    {
        goods = new Goods();
        upgradeLevel = new UpgradeLevel();
        info = new Info();
    }
}
