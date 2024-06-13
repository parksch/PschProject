using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [SerializeField] Goods goods;
    [SerializeField] UpgradeLevel upgradeLevel;

#region Datas

    [System.Serializable]
    public class Goods
    {
        public long gold;
        public long diamond;
    }

    [System.Serializable]
    public class UpgradeLevel 
    {
        public int attack;
        public int defense;
        public int hpRegen;
    }

    [System.Serializable]
    public class Info
    {
        public int stage;
    }

#endregion

    public void Init()
    {
        goods.gold = 0;
        goods.diamond = 0;
        upgradeLevel.attack = 0;
        upgradeLevel.defense = 0;
        upgradeLevel.hpRegen = 0;
    }
}
