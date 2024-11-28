using JsonClass;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Localization.Editor;
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

    DataManager.Skill target;

    public void SetInfo(DataManager.Skill skillData)
    {
        LocalizationScriptable localization = ScriptableManager.Instance.Get<LocalizationScriptable>(ScriptableType.Localization);

        target = skillData;
        icon.sprite = target.data.Sprite();
        skillName.text = ResourcesManager.Instance.GradeColorText(target.data.Grade(), localization.Get(target.data.nameKey));
        UpdateInfo();
    }

    public void UpdateInfo()
    {

    }
}
