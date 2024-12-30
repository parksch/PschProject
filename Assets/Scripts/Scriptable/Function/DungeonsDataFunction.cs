using System.Collections.Generic;
using UnityEditor.AdaptivePerformance.Editor;
using UnityEngine;

namespace JsonClass
{
    public partial class DungeonsDataScriptable // This Class is a functional Class.
    {

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

        public ClientEnum.Reward Reward()
        {
            return (ClientEnum.Reward)itemType;
        }

        public int Value(int level)
        {
            int result = 0;

            switch ((ClientEnum.ChangeType)changeType)
            {
                case ClientEnum.ChangeType.Sum:
                    break;
                case ClientEnum.ChangeType.Product:
                    break;
                default:
                    break;
            }

            return result;
        }
    }

}
