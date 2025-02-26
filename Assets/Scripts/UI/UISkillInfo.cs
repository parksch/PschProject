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
        
    DataManager.Skill target;
    string debuffKey = "DebuffKey";
    string buffKey = "BuffKey";

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
        desc.text = string.Format(descLocal, localization.Get(EnumString<State>.ToString(target.GetState())), target.GetValue() * 100f) + "\n";

        if (target.data.skillBuffs.Count > 0)
        {
            BuffDataScriptable buffData = ScriptableManager.Instance.Get<BuffDataScriptable>(ScriptableType.BuffData);

            for (int i = 0; i < target.data.skillBuffs.Count; i++)
            {
                SkillBuffs buffs = target.data.skillBuffs[i];
                
                if (buffs.state == "")
                {
                    break;
                }

                BuffData data = buffData.Get(buffs.state);
                string buffStr = string.Empty;

                if (data.IsDebuff())
                {
                    buffStr = string.Format(localization.Get(debuffKey),
                        localization.Get(EnumString<ClientEnum.CharacterType>.ToString(buffs.CharacterType())),
                        localization.Get(EnumString<ClientEnum.State>.ToString(data.State())),
                        buffs.value * 100f,
                        buffs.timer) + "\n";
                }
                else
                {
                    buffStr = string.Format(localization.Get(buffKey),
                        localization.Get(EnumString<ClientEnum.CharacterType>.ToString(buffs.CharacterType())),
                        localization.Get(EnumString<ClientEnum.State>.ToString(data.State())),
                        buffs.value * 100f,
                        buffs.timer) + "\n";
                }

                desc.text += buffStr;
            }
        }

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

        lockObject.SetActive(target.lv <= 0);
        equip.SetInterractable(target.lv != 0);
    }

}
