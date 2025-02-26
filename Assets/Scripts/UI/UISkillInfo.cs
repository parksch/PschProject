using ClientEnum;
using JsonClass;
using UnityEngine;
using UnityEngine.UI;

public class UISkillInfo : MonoBehaviour
{
    [SerializeField, ReadOnly] Image icon;
    [SerializeField, ReadOnly] Text skillName;
    [SerializeField, ReadOnly] Text lvPiece;
    [SerializeField, ReadOnly] Text desc;
    [SerializeField, ReadOnly] GameObject lockObject;
    [SerializeField, ReadOnly] UIButton amplification;
    [SerializeField, ReadOnly] UIButton upgrade;
    [SerializeField, ReadOnly] UIButton equip;
    [SerializeField] Color red;
        
    DataManager.Skill target;

    public void SetInfo(DataManager.Skill skillData)
    {
        target = skillData;
        icon.sprite = target.data.Sprite();
        UpdateInfo();
    }

    public void UpdateInfo()
    {
        LocalizationScriptable localization = ScriptableManager.Instance.Get<LocalizationScriptable>(ScriptableType.Localization);
        skillName.text = ResourcesManager.Instance.GradeColorText(target.data.Grade(), localization.Get(target.data.nameKey)) + "+" + target.amplification;
        lvPiece.text = "Lv " + target.lv;

        string descLocal = localization.Get(target.data.descKey);

        SetDesc(descLocal,localization.Get(EnumString<State>.ToString(target.GetState())),target.GetValue() * 100f);

        if (target.lv < target.data.levelMax)
        {
            amplification.gameObject.SetActive(false);
            upgrade.gameObject.SetActive(true);
            upgrade.SetInterractable(target.piece >= (1 + (target.lv * target.data.GetPiece())));
        }
        else
        {
            int need = (int)ScriptableManager.Instance.Get<DefaultValuesScriptable>(ScriptableType.DefaultValues).Get("NeedAmplification");
            upgrade.gameObject.SetActive(false);
            amplification.gameObject.SetActive(true);
            amplification.SetInterractable(DataManager.Instance.CheckGoods(ClientEnum.Goods.Amplification, need)  && target.amplification < target.data.amplificationMax);
        }

        equip.SetInterractable(target.lv != 0);
    }

    void SetDesc(string targetStr,params object[] args)
    {
        desc.text = string.Format(targetStr, args);
    }

}
