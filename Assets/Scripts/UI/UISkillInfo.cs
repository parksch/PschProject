using JsonClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISkillInfo : MonoBehaviour
{
    [SerializeField, ReadOnly] Image icon;
    [SerializeField, ReadOnly] Text skillName;
    [SerializeField, ReadOnly] Text lvPiece;
    [SerializeField, ReadOnly] Text desc;
    [SerializeField, ReadOnly] GameObject lockObject;
    [SerializeField, ReadOnly] UIButton upgrade;
    [SerializeField, ReadOnly] UIButton equip;

    SkillData target;

    public void SetInfo(SkillData skillData)
    {
        target = skillData;

        UpdateInfo();
    }

    public void UpdateInfo()
    {

    }
}
