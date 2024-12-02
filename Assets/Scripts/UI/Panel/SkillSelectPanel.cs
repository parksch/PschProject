using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSelectPanel : BasePanel
{
    [SerializeField, ReadOnly] List<UISkillSelectSlot> slots;
    
    DataManager.Skill target;
   
    public void SetTarget(DataManager.Skill skill)
    {
        target = skill;
    }

    public override void Open()
    {
        base.Open();
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].Set();
        }
    }

    public void SetSkill(int index)
    {
        DataManager.Instance.OnChangeSkill(index, target);
        UIManager.Instance.BackPanel();
    }
}
