using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class StageScriptable : ScriptableObject
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
        public List<StageMonsters> stageMonsters;
        public string nameKey;
    }

    [System.Serializable]
    public partial class StageMonsters
    {
        public List<string> monsters;
    }

}
