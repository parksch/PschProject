using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class BuffDataScriptable // This Class is a functional Class.
    {
        public BuffData Get(string id) { return buffData.Find(x => x.name == id); }
    }

    public partial class BuffData // This Class is a functional Class.
    {
        public ClientEnum.State State()
        {
            return (ClientEnum.State)state;
        }

        public Sprite Sprite()
        {
            return ResourcesManager.Instance.GetSprite(atlas, sprite);
        }

        public bool IsDebuff()
        {
            return isDebuff == 1;
        }
    }

}
