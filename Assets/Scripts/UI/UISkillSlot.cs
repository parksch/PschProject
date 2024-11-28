using JsonClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISkillSlot : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] DataManager.Skill target;


    public void SetSkill(DataManager.Skill skill)
    {
        target = skill;
        UpdateSlot();
    }

    public void UpdateSlot()
    {

    }
}
