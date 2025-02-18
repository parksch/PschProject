using ClientEnum;
using JsonClass;
using System;
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
    string key = "Guide";
    int currentValue = 0;

    string TitleLocal => ScriptableManager.Instance.Get<LocalizationScriptable>(ScriptableType.Localization).Get(titleLocal);

    public void Init()
    {
        GuideDataScriptable guideData = ScriptableManager.Instance.Get<GuideDataScriptable>(ScriptableType.GuideData);

        if (PlayerPrefs.HasKey(key))
        {
            int code = PlayerPrefs.GetInt(key);
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
            //PlayerPrefs.SetInt(key, first.id);
            SetGuide(first);
        }

    }

    public void OnClick()
    {
        if (target.guideValue > currentValue)
        {
            switch (target.GuideType())
            {
                case GuideType.Number:
                    break;
                default:
                    break;
            }

            return;
        }

        RewardPanel reward = UIManager.Instance.Get<RewardPanel>();

        switch (target.RewardType())
        {
            case Reward.Item:
                Shops shops = ScriptableManager.Instance.Get<DrawScriptable>(ScriptableType.Draw).Target();
                BaseItem item = ScriptableManager.Instance.Get<ItemDataScriptable>(ScriptableType.ItemData).GetItem(target.Item(), shops.Grade());
                DataManager.Instance.AddItem(item);
                reward.AddItem(item);
                break;
            case Reward.Goods:
                DataManager.Instance.AddGoods(target.Goods(), target.rewardValue);
                reward.AddGoods(target.Goods(), target.rewardValue);
                break;
            default:
                break;
        }

        PlayerPrefs.SetInt(key, target.next);

        if (target.next == -1)
        {
            gameObject.SetActive(false);
        }
        else
        {
            target = ScriptableManager.Instance.Get<GuideDataScriptable>(ScriptableType.GuideData).GetData(target.next);
            SetGuide(target);
        }
    }

    public void AddGuideValue(GuideType guideType,GuideKey guideKey, string code, int value = 1)
    {
        if (guideType == target.GuideType() && guideKey == target.GuideKey() && code == target.guideName)
        {
            currentValue += value;
        }

        CheckGuide(guideType,guideKey,code);
    }

    void SetGuide(GuideData guideData)
    {
        target = guideData;
        slot.SetTypeItem(target.RewardType(),target.rewardIndex,target.rewardValue);
        title.text = string.Format(TitleLocal, target.id);
        currentValue = 0;

        CheckGuide(target.GuideType(), target.GuideKey(),target.guideName);
    }

    void CheckGuide(GuideType guideType,GuideKey guideKey, string code)
    {
        if (guideType != target.GuideType())
        {
            return ;
        }

        float value = GetValue(guideType, code);

        desc.text = string.Format(target.Description(),target.guideValue,value);
        slider.value = value/target.guideValue;
        return ;
    }

    float GetValue(GuideType guideType, string code)
    {
        switch (target.GuideType())
        {
            case GuideType.Number:
                currentValue = DataManager.Instance.GetUpgradeLevel(code);
                break;
            default:
                break;
        }

        return currentValue;
    }

}
