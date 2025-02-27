using ClientEnum;
using JsonClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystemDescriptor;

public class UIRewardSlot : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] UIText text;
    Goods goods;
    int num;

    public void ResetReward()
    {
        goods = Goods.None;
        num = 0;
    }

    public bool CheckGoods(Goods target,int value)
    {
        if (target == goods)
        {
            num += value;
            text.SetText(num);
            return true;
        }

        return false;
    }

    public void SetSkill(SkillData skillData,int num)
    {
        goods = ClientEnum.Goods.None;
        image.sprite = skillData.Sprite();
        text.SetText(num);
        gameObject.SetActive(true);
    }

    public void SetItem(BaseItem item)
    {
        goods = ClientEnum.Goods.None;
        image.sprite = item.GetSprite;
        text.SetText("Lv " + item.Level);
        gameObject.SetActive(true);
    }

    public void SetGoods(Goods target,int value)
    {
        goods = target;
        num = value;
        image.sprite = ResourcesManager.Instance.GetGoodsSprite(goods);
        text.SetText(num);
        gameObject.SetActive(true); 
    }

    public void SetTypeItem(Reward reward,int index,int value)
    {
        switch (reward)
        {
            case Reward.Item:
                goods = Goods.None;
                break;
            case Reward.Goods:
                SetGoods((Goods)index, value);
;                break;
            default:
                break;
        }
    }

}
