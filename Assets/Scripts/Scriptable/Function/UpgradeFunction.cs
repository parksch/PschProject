using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class UpgradeScriptable // This Class is a functional Class.
    {

        public Upgrade GetUpgradeState(string name)
        {
            return upgrade.Find(x => x.name == name);
        }

        public List<Upgrade> GetUpgradeType(ClientEnum.UpgradeType upgradeType)
        {
            return upgrade.FindAll(x => (ClientEnum.UpgradeType)x.upgradeType == upgradeType);
        }

        public Upgrade GetUpgrade(ClientEnum.State state, ClientEnum.ChangeType upgradeType)
        {
            return upgrade.Find(x => x.State() == state && x.ChangeType() == upgradeType);
        }
    }

    public partial class Upgrade // This Class is a functional Class.
    {
        public Sprite GetSprite()
        {
            return ResourcesManager.Instance.GetSprite(atlas, sprite);
        }

        public ClientEnum.Goods Goods()
        {
            return (ClientEnum.Goods)goodsType;
        }

        public ClientEnum.State State()
        {
            return (ClientEnum.State)upgradeState;
        }

        public ClientEnum.ChangeType ChangeType()
        {
            return (ClientEnum.ChangeType)changeType;
        }

        public float GetLevelValue(int level)
        {
            float result = 0;

            for (int i = 0; i < level; i++)
            {
                result += addValue;
            }

            return result;
        }
    }
}
