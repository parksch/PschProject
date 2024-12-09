using ClientEnum;
using JsonClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class DataManager //Skill
{
    [SerializeField, ReadOnly] List<Skill> skills = new List<Skill>();
    [SerializeField, ReadOnly] List<Skill> equipSkill = new List<Skill>();

    public delegate void ChangeSkill(int index,Skill skill);

    public ChangeSkill OnChangeSkill;

    public List<Skill> Skills => skills;
    public List<Skill> EquipSkill => equipSkill;

    [System.Serializable]
    public class Skill
    {
        public SkillData data = null;
        public int lv = 0;
        public int piece = 0;

        public float GetValue()
        {
            return data.startValue + (data.addValue * lv);
        }

        public State GetState() => data.State();
    }

    void SkillInit()
    {
        List<SkillData> datas = ScriptableManager.Instance.Get<SkillDataScriptable>(ScriptableType.SkillData).skillData;

        for (int i = 0; i < datas.Count; i++)
        {
            Skill skill = new Skill();
            skill.data = datas[i];
            skill.lv = 1;
            skill.piece = 0;
            skills.Add(skill);
        }

        OnChangeSkill = null;
        OnChangeSkill += SetSkill;
    }

    void SetSkill(int index,Skill skill)
    {
        for (int i = 0; i < equipSkill.Count; i++)
        {
            if (i != index && equipSkill[i].data != null && skill.data.id == equipSkill[i].data.id)
            {
                equipSkill[i] = new Skill();
            }
        }

        equipSkill[index] = skill;
    }
}
