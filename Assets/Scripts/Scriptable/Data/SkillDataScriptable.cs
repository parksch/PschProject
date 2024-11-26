using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class SkillDataScriptable : ScriptableObject
    {
        public List<SkillData> skillData = new List<SkillData>();
    }

    [System.Serializable]
    public partial class SkillData
    {
        public string id;
        public string sprite;
        public string nameKey;
        public string descKey;
        public List<string> spellList;
        public int levelMax;
        public int grade;
        public float coolTime;
    }

}
