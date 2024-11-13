using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class StageDataScriptable // This Class is a functional Class.
    {
        public StageData Get(int index) => stageData.Find(x => x.index == index);
    }

    public partial class StageData // This Class is a functional Class.
    {
    }

    public partial class StageRewards // This Class is a functional Class.
    {
    }

}
