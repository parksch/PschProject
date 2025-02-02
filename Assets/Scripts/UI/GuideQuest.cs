using UnityEngine;

//using EnumKey = global::GuideQuest.Key;
//using EnumType = global::GuideQuest.Type;

public class GuideQuest 
{
    //[SerializeField] int currentIndex = 1;

    //[Header("Has Data")]
    //[SerializeField, ReadOnly] long currentCount;
    //[SerializeField, ReadOnly] long clearCount = 0;
    //[SerializeField, ReadOnly] EnumType type = EnumType.None;
    //[SerializeField, ReadOnly] EnumKey key = EnumKey.None;
    //[SerializeField, ReadOnly] bool isEnd = false;
    //[SerializeField, ReadOnly] bool isClear = false;

    //public delegate void OnChangeValue<T>(T value);
    //public OnChangeValue<int> onChangeCurrentIndex;
    //public OnChangeValue<long> onChangeCurrentCount;

    //public int CurrentIndex
    //{
    //    get => currentIndex;
    //    private set
    //    {
    //        currentIndex = value;
    //        onChangeCurrentIndex?.Invoke(currentIndex);
    //    }
    //}
    //public bool IsEnd => isEnd;
    //public bool IsClear => isClear;
    //public long CurrentCount
    //{
    //    get => CurrentCount;
    //    private set
    //    {
    //        currentCount = value;
    //        onChangeCurrentCount?.Invoke(currentCount);
    //    }
    //}
    //public long ClearCount => clearCount;

    //public void Start()
    //{
    //    //== Call Back Setting

    //    //== Level UP
    //    DataManager.Instance.onChangePlayerLevel += (_) => CallQuest(EnumType.ReachLevel, EnumKey.Character);
    //    FarmingSystem.Instance.Bag.onLevelUpCallback += (_) => CallQuest(EnumType.ReachLevel, EnumKey.Bag);

    //    //== Equip [ Not have Jewel ]
    //    DataManager.Instance.onChangeEquipWings += (_) => CallQuest(EnumType.Equip, EnumKey.Wing);
    //    DataManager.Instance.onChangeEquipPets += (_) => CallQuest(EnumType.Equip, EnumKey.Pet);

    //    //== Summon
    //    SummonPanel summonPanel = UIManager.Instance.GetPanel<SummonPanel>();
    //    if (summonPanel != null)
    //    {
    //        summonPanel.onSummonComplete += (target) =>
    //        {
    //            try
    //            {
    //                EnumKey enumKey = System.Enum.Parse<EnumKey>(target);
    //                CallQuest(EnumType.Summon, enumKey);
    //                CallQuest(EnumType.SummonLifeTime, enumKey);
    //            }
    //            catch (System.Exception)
    //            {
    //                DebugUtil.LogError($"{target} is can't find : Guide Quest Key\n" +
    //                    $"Check [ SummonCost Table ]");
    //            }
    //        };
    //    }
    //    else
    //    {
    //        DebugUtil.LogError($"UIManager not have SummonPanel");
    //    }

    //    //== Stage Clear
    //    GameManager.Instance.onClearStage += (_) => CallQuest(EnumType.ClearStage, EnumKey.None);

    //    //== Dungeon Reach / Clear
    //    GameManager.Instance.onClearDungeon += (key) =>
    //    {
    //        try
    //        {
    //            EnumKey enumKey = System.Enum.Parse<EnumKey>(key);
    //            CallQuest(EnumType.DungeonReach, enumKey);
    //            CallQuest(EnumType.DungeonClear, enumKey);
    //        }
    //        catch (System.Exception)
    //        {
    //            DebugUtil.LogError($"{key} is can't find : Guide Quest Key\n" +
    //                $"Check [ Dungen / Guide Quest Table ]");
    //        }

    //    };
    //    DungeonPanel dungeon = UIManager.Instance.GetPanel<DungeonPanel>();
    //    if (dungeon != null)
    //    {
    //        dungeon.onFinishSweep += (key, count) =>
    //        {
    //            try
    //            {
    //                EnumKey enumKey = System.Enum.Parse<EnumKey>(key);
    //                CallQuest(EnumType.DungeonClear, enumKey, count);
    //            }
    //            catch (System.Exception)
    //            {
    //                DebugUtil.LogError($"{key} is can't find : Guide Quest Key\n" +
    //                    $"Check [ Dungen / Guide Quest Table ]");
    //            }
    //        };
    //    }
    //    else
    //    {
    //        DebugUtil.LogError($"UIManager not have DungeonPanel");
    //    }

    //    //== Gold Upgrade
    //    DataManager.Instance.onChangeState += (key, _) =>
    //    {
    //        try
    //        {
    //            EnumKey enumKey = System.Enum.Parse<EnumKey>(((ClientEnums.UpgradeKey)key).ToString());
    //            CallQuest(EnumType.GoldUpgradeKey, enumKey);
    //        }
    //        catch (System.Exception)
    //        {
    //            DebugUtil.LogError($"{((ClientEnums.UpgradeKey)key)} is can't find : Guide Quest Key\n" +
    //                $"Check [ GuideQuest Table ]\n" +
    //                $"Gold Upgrade");
    //        }

    //    };

    //    //== Awake Upgrade
    //    DataManager.Instance.onChangeAwakeState += (key, _) =>
    //    {
    //        try
    //        {
    //            EnumKey enumKey = System.Enum.Parse<EnumKey>(((ClientEnums.UpgradeKey)key).ToString());
    //            CallQuest(EnumType.ExpUpgradeKey, enumKey);
    //        }
    //        catch (System.Exception)
    //        {
    //            DebugUtil.LogError($"{((ClientEnums.UpgradeKey)key)} is can't find : Guide Quest Key\n" +
    //                $"Check [ GuideQuest Table ]\n" +
    //                $"Awake Upgrade");
    //        }
    //    };

    //    //== Compose + OptionReroll + Jewel [ Equip / Break Down / ReinForce ] + Wing [ ReinForce ]
    //    Inventory.Panel inventory = UIManager.Instance.GetPanel<Inventory.Panel>();
    //    if (inventory != null)
    //    {
    //        inventory.onMixFinishCallback += (count) => CallQuest(EnumType.Compose, EnumKey.Wing, count);
    //        inventory.InfomationUI.onMixFinishCallback += (count) => CallQuest(EnumType.Compose, EnumKey.Wing, count);
    //        inventory.InfomationUI.onEquipJewelCallback += () => CallQuest(EnumType.Equip, EnumKey.Jewel);
    //        inventory.InfomationUI.onRerollFinish += () => CallQuest(EnumType.OptionReroll, EnumKey.None);
    //        inventory.InfomationUI.onDecompositionCallback += (count) => CallQuest(EnumType.BreakDown, EnumKey.Jewel, count);
    //        inventory.InfomationUI.onJewelLevelUpFinish += () => CallQuest(EnumType.ReinForce, EnumKey.Jewel);
    //        inventory.InfomationUI.onWingLevelUpFinish += () => CallQuest(EnumType.ReinForce, EnumKey.Wing);
    //    }

    //    //== ReinForce : Pet
    //    Inventory.PetPanel petPanel = UIManager.Instance.GetPanel<Inventory.PetPanel>();
    //    if (petPanel != null)
    //    {
    //        petPanel.onFinishLevelUP += (count) => CallQuest(EnumType.ReinForce, EnumKey.Pet, count);
    //    }
    //    else
    //    {
    //        DebugUtil.LogError($"UIManager not have PetPanel");
    //    }

    //    //== ReinForce : MysticalPet
    //    SpiritPanel spiritPanel = UIManager.Instance.GetPanel<SpiritPanel>();
    //    if (spiritPanel != null)
    //    {
    //        spiritPanel.onFinishLevelUP += (count) => CallQuest(EnumType.ReinForce, EnumKey.MysticalPet, count);
    //    }
    //    else
    //    {
    //        DebugUtil.LogError($"UIManager not have SpiritPanel");
    //    }

    //    //== Break Down : FarmingItem
    //    FarmingSystem.Instance.onDecomposition += (_, _, _, count) =>
    //    {
    //        CallQuest(EnumType.BreakDown, EnumKey.FarmingItem, count);
    //    };
    //    FarmingSystem.Instance.onDecompositions += (_, _, _, count) =>
    //    {
    //        int totalCount = 0;
    //        for (int i = 0; i < count.Count; i++)
    //        {
    //            totalCount += count[i];
    //        }
    //        CallQuest(EnumType.BreakDown, EnumKey.FarmingItem, totalCount);
    //    };

    //    //== KillEnemy
    //    GameManager.Instance.onKillEnemy += () => CallQuest(EnumType.KillEnemy, EnumKey.None);
    //}

    //public void Set()
    //{
    //    if (global::GuideQuest.table.ContainsKey(CurrentIndex))
    //    {
    //        global::GuideQuest data = global::GuideQuest.table[CurrentIndex];

    //        CurrentCount = 0;
    //        clearCount = data.value;
    //        type = System.Enum.Parse<EnumType>(data.type);
    //        if (data.key.CompareTo(string.Empty) != 0)
    //        {
    //            key = System.Enum.Parse<EnumKey>(data.key);
    //        }
    //        else
    //        {
    //            key = EnumKey.None;
    //        }
    //        isClear = Verification();
    //    }
    //    else
    //    {
    //        isEnd = true;
    //    }
    //}

    //public void CallQuest(EnumType type, EnumKey key, long add = 1)
    //{
    //    if (this.type == type && this.key == key)
    //    {
    //        CountAction(type, () =>
    //        {
    //            CurrentCount += add;
    //            return true;
    //        });

    //        isClear = Verification();
    //    }
    //}

    //public bool Verification()
    //{
    //    switch (type)
    //    {
    //        case EnumType.Equip:
    //            switch (key)
    //            {
    //                case EnumKey.Wing:
    //                    int equipWingCount = DataManager.Instance.GetEquipWingsCount();
    //                    CurrentCount = equipWingCount;
    //                    return Return();
    //                case EnumKey.Pet:
    //                    int equipPetCount = DataManager.Instance.GetEquipPetsCount();
    //                    CurrentCount = equipPetCount;
    //                    return Return();
    //                case EnumKey.Jewel:
    //                    int equipJewelCount = DataManager.Instance.GetEquipJewelCount();
    //                    CurrentCount = equipJewelCount;
    //                    return Return();
    //            }
    //            DebugUtil.LogError($"{type} Quest not find {key}");
    //            return false;
    //        case EnumType.DungeonReach:
    //            switch (key)
    //            {
    //                case EnumKey.GoldDungeon:
    //                case EnumKey.ReinforceDungeon:
    //                    CurrentCount = DataManager.Instance.DungeonInfo[key.ToString()].lastLevel;
    //                    return Return();
    //                case EnumKey.ElementalDungeon:
    //                    CurrentCount = DataManager.Instance.GetElementalDungeonLastLevel();
    //                    return Return();
    //            }
    //            return false;
    //        case EnumType.ClearStage:
    //            CurrentCount = DataManager.Instance.Record.normal;
    //            return Return();
    //        case EnumType.GoldUpgradeKey:
    //            CurrentCount = DataManager.Instance.GetCurrentState((int)ClientEnums.ToUpgradeKey(key.ToString()));
    //            return Return();
    //        case EnumType.ExpUpgradeKey:
    //            CurrentCount = DataManager.Instance.GetAwakeStatLevel((int)ClientEnums.ToUpgradeKey(key.ToString()));
    //            return Return();
    //        case EnumType.ReachLevel:
    //            switch (key)
    //            {
    //                case EnumKey.Character:
    //                    CurrentCount = DataManager.Instance.PlayerLevel;
    //                    return Return();
    //                case EnumKey.Bag:
    //                    CurrentCount = FarmingSystem.Instance.Bag.Level;
    //                    return Return();
    //            }
    //            DebugUtil.LogError($"{type} Quest not find {key}");
    //            return false;
    //        case EnumType.SummonLifeTime:
    //            CurrentCount = DataManager.Instance.GetAccumulatedSummonExp(key.ToString());
    //            return Return();

    //    }

    //    return CountAction(type, Return);

    //    bool Return()
    //    {
    //        if (clearCount <= CurrentCount)
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }
    //}

    //public bool CountAction(EnumType type, System.Func<bool> action)
    //{
    //    switch (type)
    //    {
    //        case EnumType.KillEnemy:
    //        case EnumType.DungeonClear:
    //        case EnumType.ReinForce:
    //        case EnumType.Summon:
    //        case EnumType.OpenFarmingItem:
    //        case EnumType.Compose:
    //        case EnumType.BreakDown:
    //        case EnumType.OptionReroll:
    //            return action();
    //    }

    //    return false;
    //}

    //public (string, int) Clear()
    //{
    //    if (isClear)
    //    {
    //        if (global::GuideQuest.table.ContainsKey(CurrentIndex))
    //        {
    //            global::GuideQuest data = global::GuideQuest.table[CurrentIndex];
    //            CurrentIndex++;
    //            Set();
    //            return (data.reward, data.rewardCount);
    //        }
    //        else
    //        {
    //            return (string.Empty, 0);
    //        }
    //    }
    //    return (string.Empty, 0);
    //}
}
