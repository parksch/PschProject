using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class BuffDataScriptable // This Class is a functional Class.
    {
    }

    public partial class BuffData // This Class is a functional Class.
    {
        public Sprite Sprite()
        {
            return ResourcesManager.Instance.GetSprite(atlas, sprite);
        }
    }

}
