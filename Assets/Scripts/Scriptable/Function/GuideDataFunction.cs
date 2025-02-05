using ClientEnum;
using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class GuideDataScriptable // This Class is a functional Class.
    {
        public GuideData GetData(int code)
        {
            return guideData.Find(x => x.id == code);
        }

        public GuideData First()
        {
            return guideData[0];
        }

    }

    public partial class GuideData // This Class is a functional Class.
    {
        public GuideType GuideType()
        {
            return (GuideType)guideType;
        }

        public Reward RewardType()
        {
            return (Reward)rewardType;
        }

        public Goods Goods()
        {
            return (Goods)rewardIndex;
        }

        public Item Item()
        {
            return (Item)rewardIndex;
        }

        public string Description()
        {
            return ScriptableManager.Instance.Get<LocalizationScriptable>(ScriptableType.Localization).Get(local);
        }
    }

}
