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
        public float multiplyperStageExp;
        public float multiplyPerStageHp;
        public float multiplyPerStageDefanse;
        public float multiplyPerStageAttack;
        public float multiplyPerGold;
        public float scrapProbability;
        public float multiplyBossHp;
        public float multiplyBossDefense;
        public float multiplyBossAttack;
        public float multiplyBossSize;
    }

}
