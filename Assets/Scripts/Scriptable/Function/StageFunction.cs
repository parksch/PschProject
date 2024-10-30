using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class StageScriptable // This Class is a functional Class.
    {
        public List<string> GetMonsters(int stage)
        {
            List<string> result = null;

            if (stage > stageMonsters.Count)
            {
                result = stageMonsters[stageMonsters.Count - 1].monsters;
            }
            else
            {
                result = stageMonsters[stage].monsters;
            }

            return result;
        }
    }

    public partial class StageMonsters
    {
    }

}
