using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Scriptable/Skill")]
public class SkillScriptable : BaseScriptable
{
    [SerializeField] List<SkillData> skillDataList;

    [System.Serializable]
    public class SkillData
    {
        public string name;
        public string description;
        public int levelMax;
        public int maxAttackCount;
        public float damageCycleTime;
        public float damageMultiply;
        public float upgradeAddPer;
        public float coolTime;
        public Sprite sprite;
    }

}
