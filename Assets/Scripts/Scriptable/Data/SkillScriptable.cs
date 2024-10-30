using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class SkillScriptable : ScriptableObject
    {
        public List<Skill> skill = new List<Skill>();
    }

    [System.Serializable]
    public partial class Skill
    {
        public string name;
        public string description;
        public int levelMax;
        public int maxAttackCount;
        public float damageCycleTime;
        public float damageMultiply;
        public float upgradeAddPer;
        public float coolTime;
        public string sprite;
    }

}
