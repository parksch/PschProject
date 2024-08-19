using System.Collections.Generic;
using Unity.Notifications.iOS;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [SerializeField] ClientEnum.Language language;
    [SerializeField, ReadOnly] string deviceNum;
    [SerializeField, ReadOnly] PlayerState playerDefaultState;
    [SerializeField, ReadOnly] List<InventoryData> inventoryDatas = new List<InventoryData>();
    [SerializeField] Goods goods;
    [SerializeField] Info info;

    Dictionary<string, int> upgradeLevel = new Dictionary<string, int>();

    public delegate void ChangeExp(float ratio);
    public delegate void ChangeGold(long gold);
    public ChangeGold OnChangeGold;
    public ChangeExp OnChangeExp;

    public ClientEnum.Language Language { set { language = value; } get { return language; } }
    public Goods GetGoods => goods;
    public int GetUpgradeLevel(string code) => upgradeLevel[code];
    public Info GetInfo => info;
    public List<InventoryData> InventoryDatas => inventoryDatas;
    public PlayerState PlayerDefaultState => playerDefaultState;

    public float ExpRatio()
    {
        UpgradeScriptable.UpgradeState exp = TableManager.Instance.UpgradeScriptable.GetUpgradeState("Exp");
        return ((float)info.currentExp / GetLevelExp(info.currentLevel + 1));
    }

    public void AddGold(long value)
    {
        goods.gold += value;

        OnChangeGold(goods.gold);
    }

    public void AddExp(long value)
    {
        info.currentExp += value;

        if (info.currentExp > GetLevelExp(TableManager.Instance.UpgradeScriptable.GetUpgradeState("Exp").maxLevel))
        {
            info.currentExp = GetLevelExp(TableManager.Instance.UpgradeScriptable.GetUpgradeState("Exp").maxLevel);
        }

        OnChangeExp(ExpRatio());
    }

    long GetLevelExp(int targetLevel)
    {
        UpgradeScriptable.UpgradeState expState = TableManager.Instance.UpgradeScriptable.GetUpgradeState("Exp");
        long exp = 0;

        for (int i = 0; i < targetLevel; i++)
        {
            exp += (long)((TableManager.Instance.StageScriptable.StartLevelExp) * (1 + (i * expState.addValue)));
        }

        return exp;
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
    }

    public void Init()
    {
        deviceNum = SystemInfo.deviceUniqueIdentifier;
        List<UpgradeScriptable.UpgradeState> upgradeStates = TableManager.Instance.UpgradeScriptable.GetUpgradeType(ClientEnum.UpgradeType.StatePanel);

        for (int i = 0; i < upgradeStates.Count; i++)
        {
            upgradeLevel[upgradeStates[i].name] = 0;
        }
    }
}
