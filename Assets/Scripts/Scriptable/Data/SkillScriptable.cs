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
        public int id;
        public string sprite;
        public string name;
        public string description;
        public int levelMax;
        public int maxAttackCount;
        public float coolTime;
        public float upgradeAddPer;
    }

}
