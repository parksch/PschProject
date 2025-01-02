using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace JsonClass
{
    public partial class DungeonsDataScriptable // This Class is a functional Class.
    {
        public DungeonsData GetDungeon(ClientEnum.GameMode gameMode)
        {
           return dungeonsData.Find(x => x.GameMode() == gameMode);
        }
    }

    public partial class DungeonsData // This Class is a functional Class.
    {
        public Sprite Sprite()
        {
            return ResourcesManager.Instance.GetSprite(atlas, sprite);
        }

        public string Title()
        {
            return ScriptableManager.Instance.Get<LocalizationScriptable>(ScriptableType.Localization).Get(nameLocal);
        }

        public ClientEnum.Goods NeedGoods()
        {
            return (ClientEnum.Goods)needGoodsType;
        }

        public ClientEnum.Reward RewardType()
        {
            return (ClientEnum.Reward)itemType;
        }

        public ClientEnum.GameMode GameMode()
        {
            return (ClientEnum.GameMode)gamemode;
        }

        public List<(int index,int value)> GetRewards(int level)
        {
            List<(int index, int value)> rewards = new List<(int index, int value)>();

            for (int i = 0; i < dungeonReward.Count; i++)
            {
                rewards.Add((dungeonReward[i].index, dungeonReward[i].Value(level)));
            }

            return rewards;
        }

        public void Monsters(List<string> strs)
        {
            for (int i = 0; i < monsters.Count; i++)
            {
                strs.Add(monsters[i]);
            }
        }
    }

    public partial class DungeonReward // This Class is a functional Class.
    {
        public int Value(int level)
        {
            int result = 0;

            switch ((ClientEnum.ChangeType)changeType)
            {
                case ClientEnum.ChangeType.Sum:
                    result = value + (int)(level * addValue);
                    break;
                case ClientEnum.ChangeType.Product:
                    result = (int)(value * (1 + (addValue * level)));
                    break;
                default:
                    break;
            }

            return result;
        }

    }
}
