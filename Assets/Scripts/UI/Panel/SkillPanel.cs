using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : BasePanel
{
    [SerializeField] RectTransform content;
    [SerializeField] List<UISkillSlot> slots = new List<UISkillSlot>();
    [SerializeField] UISkillSlot prefab;
    [SerializeField] UISkillInfo info;
    [SerializeField] SkillSelectPanel selectPanel;

    DataManager.Skill target;

    public override void OnUpdate()
    {

    }

    public override void FirstLoad()
    {
        List<DataManager.Skill> skills = DataManager.Instance.Skills;

        for (int i = 0; i < skills.Count; i++)
        {
            if (i == 0 )
            {
                slots[i].SetSkill(skills[i]);
            }
            else if(slots.Count <= i)
            {
                UISkillSlot slot = Instantiate(prefab.gameObject, content).GetComponent<UISkillSlot>();
                slot.SetSkill(skills[i]);
                slots.Add(slot);
            }
        }

        SetInfo(skills[0]);
    }

    public void SetInfo(DataManager.Skill _target)
    {
        target = _target;
        info.SetInfo(target);
    }

    public override void Open()
    {
        base.Open();
    }

    public override void Close()
    {

    }

    public void OnClickEquip()
    {
        selectPanel.SetTarget(target);
        selectPanel.CopyTopMenu(ActiveTop);
        UIManager.Instance.AddPanel(selectPanel);
    }

    public void OnClickReinforce()
    {

    }
}
