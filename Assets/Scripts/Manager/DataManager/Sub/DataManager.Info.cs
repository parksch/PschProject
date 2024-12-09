using JsonClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class DataManager //Info
{
    [SerializeField] Info info;
    public Info GetInfo => info;

    public ChangeGoods OnChangeExp;

    public float ExpRatio()
    {
        if (info.CurrentExp >= info.MaxExp)
        {
            return 1;
        }
        else
        {
            int currentLevel = info.CurrentLevel;
            return ((float)info.CurrentExp - info.LevelExp(currentLevel))/ (info.LevelExp(info.CurrentLevel + 1) - info.LevelExp(info.CurrentLevel));
        }
    }

    void AddExp(long value)
    {
        info.CurrentExp = (info.CurrentExp + value);
        
        if (info.CurrentExp >= info.MaxExp)
        {
            info.CurrentExp = info.MaxExp;
        }
    }

    long LevelExp(int targetLevel)
    {
        long exp = info.LevelExp(targetLevel);

        return exp;
    }

    [System.Serializable]
    public class Info
    {
        [SerializeField] string userName;
        [SerializeField] int stage = 0;
        [SerializeField] int challengingStage = 0;
        [SerializeField] int currentLevel;
        [SerializeField] int startExp;
        [SerializeField] long currentExp;
        [SerializeField] long maxExp;
        [SerializeField] List<Datas.Pair<string, int>> drawLimit;
        [SerializeField] List<Datas.Pair<string, int>> drawCount;
        [SerializeField] Upgrade expUpgrade;

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
        public int StartExp => startExp;
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
        public int ChallengingStage
        {
            get
            {
                return challengingStage;
            }
            set
            {
                challengingStage = value;
            }
        }
        public int CurrentLevel
        {
            get
            {
                int level = 0;
                long exp = currentExp;
                long target = 0;

                if (currentExp >= maxExp)
                {
                    return expUpgrade.maxLevel;
                }

                for (int i = 0; i < expUpgrade.maxLevel; i++)
                {
                    target += (long)(startExp * (1 + (i * expUpgrade.addValue)));

                    if (currentExp >= target)
                    {
                        level++;
                    }
                    else
                    {
                        break;
                    }
                }

                return level;
            }
        }
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
        public long MaxExp => maxExp;
        public long LevelExp(int targetLevel)
        {
            long exp = 0;

            for (int i = 0; i < targetLevel; i++)
            {
                exp += (long)(startExp * (1 + (i * expUpgrade.addValue)));
            }

            return exp;
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
        public void SetUpgrade(Upgrade exp)
        {
            expUpgrade = exp;
            startExp = expUpgrade.needGoods;
            maxExp = LevelExp(exp.maxLevel);
        }
    }

    void InfoInit()
    {
        info.Stage = 1;
        info.ChallengingStage = 1;
        info.UserName = "Player";
        info.SetUpgrade(ScriptableManager.Instance.Get<UpgradeScriptable>(ScriptableType.Upgrade).GetUpgradeState("Exp"));

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

        OnChangeExp = null;
        OnChangeExp += AddExp;
    }
}
