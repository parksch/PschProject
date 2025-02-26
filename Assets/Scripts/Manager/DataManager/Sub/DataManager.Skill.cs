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
        public int amplification = 0;
        public int maxPiece = 0;

        public int LevelPiece()
        {
            int lvPiece = 0;

            for (int i = 0; i < lv; i++)
            {
                lvPiece += (1 + (i * data.GetPiece()));
            }

            return lvPiece;
        }

        public int GetCurrentPiece()
        {
            int currentPiece = piece + LevelPiece();

            return currentPiece;
        }

        public bool CheckOver()
        {
            return GetCurrentPiece() > maxPiece;
        }

        public float GetValue()
        {
            if (lv == 0)
            {
                return 0;
            }

            return (data.startValue + (data.addValue * lv - 1)) * (1 + (.1f * amplification));
        }

        public State GetState() => data.State();

    }

    void SkillInit()
    {
        List<SkillData> dates = ScriptableManager.Instance.Get<SkillDataScriptable>(ScriptableType.SkillData).skillData;

        for (int i = 0; i < dates.Count; i++)
        {
            Skill skill = new Skill();
            skill.data = dates[i];
            skill.lv = 0;
            skill.piece = 0;

            for (int j = 0; j < skill.data.levelMax; j++)
            {
                skill.maxPiece += 1 + (j * skill.data.GetPiece());
            }

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

    public void AddPiece(SkillData skillData,int num)
    {
        Skill skill = skills.Find(x => x.data.id == skillData.id);

        skill.piece += num;

        if (skill.CheckOver())
        {
            int value = skill.GetCurrentPiece() - skill.maxPiece;
            int needValue = skill.maxPiece - skill.LevelPiece();
            skill.piece = needValue;
            AddGoods(ClientEnum.Goods.Amplification, value);
        }
    }
}
