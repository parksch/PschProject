using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class StageOptionScriptable : ScriptableObject
    {
        public int maxEnemyCount;
        public int startGold;
        public int startExp;
        public int scrapMin;
        public int scrapMax;
        public float scrapProbability;
        public float multiplierStageExp;
        public float multiplierStageHp;
        public float multiplierStageDefense;
        public float multiplierStageAttack;
        public float multiplierGold;
        public float multiplierBossHp;
        public float multiplierBossDefense;
        public float multiplierBossAttack;
        public float multiplierBossSize;
    }

}
