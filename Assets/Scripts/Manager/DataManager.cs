using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [SerializeField] ClientEnum.Language language;
    [SerializeField, ReadOnly] string deviceNum;
    [SerializeField, ReadOnly] PlayerState playerDefaultState;
    [SerializeField, ReadOnly] List<BaseItem> inventoryDatas = new List<BaseItem>();
    [SerializeField] Goods goods;
    [SerializeField] Info info;

    Dictionary<string, int> upgradeLevel = new Dictionary<string, int>();

    public delegate void ChangeGoods(long value);
    public delegate void ChangeExp(float ratio);

    public ChangeGoods OnChangeGem;
    public ChangeGoods OnChangeScrap;
    public ChangeGoods OnChangeGold;
    public ChangeExp OnChangeExp;

    public bool CheckGoods(ClientEnum.Goods type,long need)
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
    public void SetDevice(string value) => deviceNum = value;
    public ClientEnum.Language Language { set { language = value; } get { return language; } }
    public Goods GetGoods => goods;
    public int GetUpgradeLevel(string code) => upgradeLevel[code];
    public Info GetInfo => info;
    public List<BaseItem> InventoryDatas => inventoryDatas;
    public PlayerState PlayerDefaultState => playerDefaultState;

    public float ExpRatio()
    {
        UpgradeScriptable.UpgradeState exp = TableManager.Instance.UpgradeScriptable.GetUpgradeState("Exp");
        return ((float)info.CurrentExp / GetLevelExp(info.CurrentLevel + 1));
    }

    public void AddScrap(long value)
    {
        goods.scrap += value;

        OnChangeScrap(goods.scrap);
    }

    public void AddItem(BaseItem item)
    {
        inventoryDatas.Add(item);
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

    public void AddExp(long value)
    {
        info.CurrentExp += value;

        if (info.CurrentExp > GetLevelExp(TableManager.Instance.UpgradeScriptable.GetUpgradeState("Exp").maxLevel))
        {
            info.CurrentExp = GetLevelExp(TableManager.Instance.UpgradeScriptable.GetUpgradeState("Exp").maxLevel);
        }

        OnChangeExp(ExpRatio());
    }

    public void UseGoods(ClientEnum.Goods type,long value)
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
    public class Goods
    {
        public long gold = 0;
        public long gem = 0;
        public long scrap = 0;
    }

    [System.Serializable]
    public class Info
    {
        [SerializeField] string userName;
        [SerializeField] int stage = 0;
        [SerializeField] int currentLevel;
        [SerializeField] long currentExp;
        [SerializeField] List<Datas.Pair<string, int>> drawLimit;
        [SerializeField] List<Datas.Pair<string, int>> drawCount;

        public string UserName => userName;
        public int Stage => stage;
        public int CurrentLevel => currentLevel;
        public long CurrentExp
        {
            set
            {
                currentExp += value;
            }
            get
            {
                return currentExp;
            }
        }
        public int DrawLimit(string name) => drawLimit.Find(x => name == x.key).value;
        public int AddDrawLimit(string name, int value) => drawLimit.Find(x => name == x.key).value += value;
        public void CreateDrawLimit(string name)
        {
            if (drawLimit.Find(x => name == x.key) == null)
            {
                drawLimit.Add(new Datas.Pair<string,int>(name,0));
            }
        }
        public int DrawCount(string name) => drawCount.Find(x => name == x.key).value;
        public int AddDrawCount(string name, int value) => drawCount.Find(x => name == x.key).value += value;
        public void CreateDrawCount(string name)
        {
            if (drawCount.Find(x => name == x.key) == null)
            {
                drawCount.Add(new Datas.Pair<string, int>(name, 0));
            }
        }
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

        for (var i = ClientEnum.Draw.Min; i < ClientEnum.Draw.Max; i++)
        {
            DrawScriptable.Category shop = TableManager.Instance.DrawScriptable.GetData(i);

            if (shop == null)
            {
                continue;
            }

            for (int j = 0; j < shop.Datas.Count; j++)
            {
                if (shop.Datas[j].Limit > 0)
                {
                    info.CreateDrawLimit(shop.Datas[j].NameKey);
                }

                if (shop.Datas[j].MaxLevel > 0)
                {
                    info.CreateDrawCount(shop.Datas[j].NameKey);
                }
            }

        }
    }
}
