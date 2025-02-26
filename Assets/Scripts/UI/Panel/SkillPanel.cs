using JsonClass;
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

    string ampStr = "NeedAmplification";
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

        SetInfo(slot);
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
        slot.Target.piece = slot.Target.piece - (1 + (slot.Target.lv * slot.Target.data.GetPiece()));
        slot.Target.lv++;

        SetInfo(slot);
        slot.UpdateSlot();
    }

    public void OnClickAmplification()
    {
        int need = (int)ScriptableManager.Instance.Get<DefaultValuesScriptable>(ScriptableType.DefaultValues).Get(ampStr);
        DataManager.Instance.UseGoods(ClientEnum.Goods.Amplification, need);
        slot.Target.amplification++;

        SetInfo(slot);
        slot.UpdateSlot();
    }
}
