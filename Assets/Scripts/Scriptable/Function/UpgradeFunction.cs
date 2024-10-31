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
    }

    public partial class Upgrade
    {
        public Sprite GetSprite()
        {
            return ResourcesManager.Instance.GetSprite(atlas, sprite);
        }
    }

}
