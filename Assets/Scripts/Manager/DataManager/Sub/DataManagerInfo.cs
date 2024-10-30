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
        UpgradeScriptable.UpgradeState exp = ScriptableManager.Instance.Get<UpgradeScriptable>(ScriptableType.Upgrade).GetUpgradeState("Exp");
        return ((float)info.CurrentExp / GetLevelExp(info.CurrentLevel + 1));
    }

    public void AddExp(long value)
    {
        info.CurrentExp += value;

        if (info.CurrentExp > GetLevelExp(ScriptableManager.Instance.Get<UpgradeScriptable>(ScriptableType.Upgrade).GetUpgradeState("Exp").maxLevel))
        {
            info.CurrentExp = GetLevelExp(ScriptableManager.Instance.Get<UpgradeScriptable>(ScriptableType.Upgrade).GetUpgradeState("Exp").maxLevel);
        }

        OnChangeExp(ExpRatio());
    }

    long GetLevelExp(int targetLevel)
    {
        UpgradeScriptable.UpgradeState expState = ScriptableManager.Instance.Get<UpgradeScriptable>(ScriptableType.Upgrade).GetUpgradeState("Exp");
        long exp = 0;

        for (int i = 0; i < targetLevel; i++)
        {
            exp += (long)((ScriptableManager.Instance.Get<StageScriptable>(ScriptableType.Stage).startExp * 10f) * (1 + (i * expState.addValue)));
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
                drawLimit.Add(new Datas.Pair<string, int>(name, 0));
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

}
