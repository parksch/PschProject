using ClientEnum;
using JsonClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGuide : MonoBehaviour
{
    [SerializeField, ReadOnly] GuideData current;
    [SerializeField, ReadOnly] UIRewardSlot slot;
    [SerializeField, ReadOnly] Slider slider;
    [SerializeField, ReadOnly] Text title;
    [SerializeField, ReadOnly] Text desc;
    [SerializeField, ReadOnly] string titleLocal;

    GuideData target;

    string TitleLocal => ScriptableManager.Instance.Get<LocalizationScriptable>(ScriptableType.Localization).Get(titleLocal);


    public void Init()
    {
        GuideDataScriptable guideData = ScriptableManager.Instance.Get<GuideDataScriptable>(ScriptableType.GuideData);

        if (PlayerPrefs.HasKey("Guide"))
        {
            int code = PlayerPrefs.GetInt("Guide");
            if (code == -1)
            {
                gameObject.SetActive(false);
            }
            else
            {
                GuideData load = guideData.GetData(code);
                SetGuide(load);
            }
        }
        else 
        {
            GuideData first = guideData.First();
            //PlayerPrefs.SetInt("Guide", first.id);
            SetGuide(first);
        }

    }

    public void SetGuide(GuideData guideData)
    {
        target = guideData;

        switch (target.Reward())
        {
            case Reward.Item:
                break;
            case Reward.Goods:
                slot.SetGoods(target.Goods(), target.value);
                break;
            default:
                break;
        }

        title.text = string.Format(TitleLocal, target.id);

        CheckGuide(target.GuideType(), target.guideName);
    }

    public bool CheckGuide(GuideType guideType,string code)
    {
        if (guideType != target.GuideType())
        {
            return false;
        }

        bool result = false;
        float value = 0;

        switch (target.GuideType())
        {
            case GuideType.Upgrade:
                break;
            default:
                break;
        }

        desc.text = string.Format(target.Description(),target.value,value);

        slider.value = value/target.value;
        return result;
    }

    public void OnClick()
    {

    }

    public int GetValue(GuideType guideType)
    {
        int result = 0;

        return result;
    }
}
