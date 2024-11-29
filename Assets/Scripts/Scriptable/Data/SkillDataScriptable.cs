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
        public string atlas;
        public string sprite;
        public string nameKey;
        public string descKey;
        public string prefab;
        public int levelMax;
        public int grade;
        public int targetState;
        public float startValue;
        public float addValue;
        public float coolTime;
        public List<SkillBuffs> skillBuffs;
    }

    [System.Serializable]
    public partial class SkillBuffs
    {
        public int state;
        public float timer;
        public float value;
    }

}
