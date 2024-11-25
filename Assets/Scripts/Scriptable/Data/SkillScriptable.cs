using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class SkillScriptable : ScriptableObject
    {
        public List<SkillData> skillData = new List<SkillData>();
    }

    [System.Serializable]
    public partial class SkillData
    {
        public int id;
        public int levelMax;
        public int grade;
        public string sprite;
        public string nameKey;
        public string descKey;
        public float coolTime;
        public List<int> SpellList;
    }
}
