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

    UISkillSlot slot;

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

        SetInfo(slots[0]);
    }

    public void SetInfo(UISkillSlot _slot)
    {
        slot = _slot;
        info.SetInfo(slot.Target);
    }

    public override void Open()
    {
        base.Open();

        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].UpdateSlot();
        }
    }

    public override void Close()
    {

    }

    public void OnClickEquip()
    {
        selectPanel.SetTarget(slot.Target);
        selectPanel.CopyTopMenu(ActiveTop);
        UIManager.Instance.AddPanel(selectPanel);
    }

    public void OnClickReinforce()
    {
        slot.Target.lv++;
        slot.Target.piece = slot.Target.piece - (1 + (slot.Target.lv * slot.Target.data.GetPiece()));
        SetInfo(slot);
        slot.UpdateSlot();
    }
}
