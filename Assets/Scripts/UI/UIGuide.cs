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
    [SerializeField, ReadOnly] Text title;
    [SerializeField, ReadOnly] Text desc;

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
            PlayerPrefs.SetInt("Guide", first.id);
            SetGuide(first);
        }

    }

    public void SetGuide(GuideData guideData)
    {
        switch (guideData.Reward())
        {
            case Reward.Item:
                break;
            case Reward.Goods:
                slot.SetGoods(guideData.Goods(), guideData.value);
                break;
            default:
                break;
        }
    }

    public void CheckGuide(GuideType guideType,string code,int value)
    {

    }

    public void OnClick()
    {

    }
}
