using JsonClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class DataManager //Info
{
    [SerializeField] Info info;
    public Info GetInfo => info;

    public delegate void ChangeExp(float ratio);

    public ChangeExp OnChangeExp;

    public float ExpRatio()
    {
        Upgrade exp = ScriptableManager.Instance.Get<UpgradeScriptable>(ScriptableType.Upgrade).GetUpgradeState("Exp");
        return ((float)info.CurrentExp / GetLevelExp(info.CurrentLevel + 1));
    }

    public void AddExp(long value)
    {
        info.CurrentExp = (info.CurrentExp + value);

        if (info.CurrentExp > GetLevelExp(ScriptableManager.Instance.Get<UpgradeScriptable>(ScriptableType.Upgrade).GetUpgradeState("Exp").maxLevel))
        {
            info.CurrentExp = GetLevelExp(ScriptableManager.Instance.Get<UpgradeScriptable>(ScriptableType.Upgrade).GetUpgradeState("Exp").maxLevel);
        }

        OnChangeExp(ExpRatio());
    }

    long GetLevelExp(int targetLevel)
    {
        Upgrade expState = ScriptableManager.Instance.Get<UpgradeScriptable>(ScriptableType.Upgrade).GetUpgradeState("Exp");
        long exp = 0;

        for (int i = 0; i < targetLevel; i++)
        {
            exp += (long)((ScriptableManager.Instance.Get<StageOptionScriptable>(ScriptableType.StageOption).startExp * 10f) * (1 + (i * expState.addValue)));
        }

        return exp;
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

        public string UserName 
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
            }
        }
        public int Stage
        {
            get
            {
                return stage;
            }
            set
            {
                stage = value;
            }
        }
        public int CurrentLevel => currentLevel;
        public long CurrentExp
        {
            set
            {
                currentExp = value;
            }
            get
            {
                return currentExp;
            }
        }
        public int DrawLimit(string name) => drawLimit.Find(x => name == x.key).value;
        public int DrawCount(string name) => drawCount.Find(x => name == x.key).value;
        public int AddDrawLimit(string name, int value) => drawLimit.Find(x => name == x.key).value += value;
        public int AddDrawCount(string name, int value) => drawCount.Find(x => name == x.key).value += value;
        public void CreateDrawLimit(string name)
        {
            if (drawLimit.Find(x => name == x.key) == null)
            {
                drawLimit.Add(new Datas.Pair<string, int>(name, 0));
            }
        }
        public void CreateDrawCount(string name)
        {
            if (drawCount.Find(x => name == x.key) == null)
            {
                drawCount.Add(new Datas.Pair<string, int>(name, 0));
            }
        }
    }

    void InfoInit()
    {
        info.Stage = 1;
        info.UserName = "Player";

        for (var i = ClientEnum.Draw.Min; i < ClientEnum.Draw.Max; i++)
        {
            Draw shop = ScriptableManager.Instance.Get<DrawScriptable>(ScriptableType.Draw).GetData(i);

            if (shop == null)
            {
                continue;
            }

            for (int j = 0; j < shop.type.shops.Count; j++)
            {
                if (shop.type.shops[j].limit > 0)
                {
                    info.CreateDrawLimit(shop.type.shops[j].nameKey);
                }

                if (shop.type.shops[j].maxLevel > 0)
                {
                    info.CreateDrawCount(shop.type.shops[j].nameKey);
                }
            }
        }
    }
}
