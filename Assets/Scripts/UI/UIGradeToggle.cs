using ClientEnum;
using JsonClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGradeToggle : MonoBehaviour
{
    [SerializeField, ReadOnly] Grade target;
    [SerializeField, ReadOnly] Text gradeText;
    [SerializeField, ReadOnly] Toggle toggle;

    public Grade Grade => target;
    public bool IsOn => toggle.isOn;

    public void Set(Grade grade)
    {
        target = grade;
        LocalizationScriptable local = ScriptableManager.Instance.Get<LocalizationScriptable>(ScriptableType.Localization);

        gradeText.text = $"<color={ResourcesManager.Instance.GradeColor(target)}>{local.Get(EnumString<Grade>.ToString(target))}</color>";
    }
}
