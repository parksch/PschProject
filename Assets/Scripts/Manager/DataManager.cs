using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [SerializeField, ReadOnly] string deviceNum;
    [SerializeField, ReadOnly] PlayerState playerDefaultState;
    [SerializeField, ReadOnly] List<InventoryData> inventoryDatas = new List<InventoryData>();
    [SerializeField] Goods goods;
    [SerializeField] UpgradeLevel upgradeLevel;
    [SerializeField] Info info;

    public delegate void ChangeExp(float ratio);
    public delegate void ChangeGold(long gold);
    public ChangeGold OnChangeGold;
    public ChangeExp OnChangeExp;

    public Goods GetGoods => goods;
    public UpgradeLevel GetUpgradeLevel => upgradeLevel;
    public Info GetInfo => info;
    public List<InventoryData> InventoryDatas => inventoryDatas;
    public PlayerState PlayerDefaultState => playerDefaultState;

    public float ExpRatio()
    {
        UpgradeScriptable.UpgradeState exp = TableManager.Instance.UpgradeScriptable.GetUpgradeState("Exp");
        return ((float)info.currentExp / (long)(exp.maxLevel * exp.addValue));
    }

    public void AddGold(long value)
    {
        goods.gold += value;

        OnChangeGold(goods.gold);
    }

    public void AddExp(long value)
    {
        UpgradeScriptable.UpgradeState exp = TableManager.Instance.UpgradeScriptable.GetUpgradeState("Exp");

        info.currentExp += value;

        if (info.currentExp > exp.maxLevel * exp.addValue)
        {
            info.currentExp = (long)(exp.maxLevel * exp.addValue);
        }
        
        OnChangeExp(ExpRatio());
    }

    #region Datas

    [System.Serializable]
    public class InventoryData
    {
        public string id;
        public ClientEnum.Item itemType;
    }

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
        public long currentExp;
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
    }
}
