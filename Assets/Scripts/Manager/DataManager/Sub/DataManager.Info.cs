using ClientEnum;
using JsonClass;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public partial class DataManager //Info
{
    [SerializeField] string userName;
    [SerializeField] long currentExp;
    [SerializeField] long maxExp;
    [SerializeField] int stage = 0;
    [SerializeField] int challengingStage = 0;
    [SerializeField] int currentLevel;
    [SerializeField] int currentGoldDungeon;
    [SerializeField] int currentGemDungeon;
    [SerializeField] int startExp;
    [SerializeField] int weekly;
    [SerializeField] List<Datas.Pair<string, int>> drawLimit;
    [SerializeField] List<Datas.Pair<string, int>> drawCount;
    [SerializeField] Upgrade expUpgrade;
    [SerializeField] DateTime currentDate;

    public delegate void ChangeExp(long exp);
    public ChangeExp OnChangeExp;

    public DateTime CurrentDate => currentDate;

    public string UserName
    {
        get
        {
            return userName;
        }
    }

    public float ExpRatio()
    {
        if (CurrentExp >= MaxExp)
        {
            return 1;
        }
        else
        {
            int currentLevel = CurrentLevel;
            return ((float)CurrentExp - LevelExp(currentLevel))/ (LevelExp(CurrentLevel + 1) - LevelExp(CurrentLevel));
        }
    }

    public int Weekly => weekly;
    public int CurrentGoldDungeon => currentGoldDungeon;
    public int CurrentGemDungeon => currentGemDungeon;
    public int StartExp => startExp;
    public int Stage
    {
        get
        {
            return stage;
        }
    }
    public int ChallengingStage
    {
        get
        {
            return challengingStage;
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

    public void AddWeekly() { weekly = (weekly + 1) % 7; }
    public void SetChallengingStage(int value) => challengingStage = value;
    public void SetStage(int value) => stage = value;
    public void AddDungeon(GameMode gameMode)
    {
        switch (gameMode)
        {
            case GameMode.GoldDungeon:
                currentGoldDungeon++;
                break;
            case GameMode.GemDungeon:
                currentGemDungeon++;
                break;
            default:
                break;
        }
    }
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

    void AddExp(long value)
    {
        currentExp = (currentExp + value);

        if (currentExp >= MaxExp)
        {
            currentExp = MaxExp;
        }
    }
    void InfoInit()
    {
        currentDate  = DateTime.UtcNow;
        stage = 1;
        challengingStage = 1;
        userName = "Player";
        SetUpgrade(ScriptableManager.Instance.Get<UpgradeScriptable>(ScriptableType.Upgrade).GetUpgradeState("Exp"));

        for (var i = ClientEnum.Draw.Min; i < ClientEnum.Draw.Max; i++)
        {
            JsonClass.Draw shop = ScriptableManager.Instance.Get<DrawScriptable>(ScriptableType.Draw).GetData(i);

            if (shop == null)
            {
                continue;
            }

            for (int j = 0; j < shop.type.shops.Count; j++)
            {
                if (shop.type.shops[j].limit > 0)
                {
                    CreateDrawLimit(shop.type.shops[j].nameKey);
                }

                if (shop.type.shops[j].maxLevel > 0)
                {
                    CreateDrawCount(shop.type.shops[j].nameKey);
                }
            }
        }

        OnChangeExp = null;
        OnChangeExp += AddExp;
    }
}
