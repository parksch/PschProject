using JsonClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISkillSlot : MonoBehaviour
{
    [SerializeField, ReadOnly] Text title;
    [SerializeField, ReadOnly] Text lv;
    [SerializeField, ReadOnly] Text piece;
    [SerializeField, ReadOnly] Slider slider;
    [SerializeField, ReadOnly] Image fill;
    [SerializeField, ReadOnly] Image icon;
    [SerializeField, ReadOnly] GameObject frontLock;
    [SerializeField, ReadOnly] DataManager.Skill target;
    [SerializeField, ReadOnly] Color redText;
    [SerializeField, ReadOnly] Color greenText;
    [SerializeField, ReadOnly] Color red;
    [SerializeField, ReadOnly] Color green;

    public DataManager.Skill Target => target;

    public void SetSkill(DataManager.Skill skill)
    {
        LocalizationScriptable localization = ScriptableManager.Instance.Get<LocalizationScriptable>(ScriptableType.Localization);
        target = skill;
        icon.sprite = target.data.Sprite();
        title.text = ResourcesManager.Instance.GradeColorText(target.data.Grade(), localization.Get(target.data.nameKey));
        UpdateSlot();
    }

    public void UpdateSlot()
    {
        int needValue = 1 + (target.lv * target.data.GetPiece());

        lv.text = $"Lv{target.lv}";
        slider.value = (float)target.piece / needValue;
        piece.text = $"{target.piece}/{needValue}";
        frontLock.SetActive(target.lv == 0 && target.piece == 0);

        if (target.piece >= needValue)
        {
            fill.color = green;
            piece.color = greenText;
        }
        else
        {
            fill.color = red;
            piece.color = redText;
        }
    }

    public void OnClick()
    {
        UIManager.Instance.Get<SkillPanel>().SetInfo(this);
    }
}
