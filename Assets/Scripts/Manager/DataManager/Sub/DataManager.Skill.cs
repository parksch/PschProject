using JsonClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class DataManager //Skill
{
    [SerializeField, ReadOnly] List<Skill> skills = new List<Skill>();

    public List<Skill> Skills => skills;

    [System.Serializable]
    public class Skill
    {
        public SkillData data;
        public int lv;
        public int piece;
    }

    void SkillInit()
    {
        List<SkillData> datas = ScriptableManager.Instance.Get<SkillDataScriptable>(ScriptableType.SkillData).skillData;

        for (int i = 0; i < datas.Count; i++)
        {
            Skill skill = new Skill();
            skill.data = datas[i];
            skill.lv = 0;
            skill.piece = 0;
            skills.Add(skill);
        }
    }

}
