using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRewardSlot : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] UIText text;
    ClientEnum.Goods goods;
    int num;

    public bool CheckGoods(ClientEnum.Goods target,int value)
    {
        if (target == goods)
        {
            num += value;
            text.SetText(num);
            return true;
        }

        return false;
    }

    public void SetItem(BaseItem item)
    {
        goods = ClientEnum.Goods.None;
        image.sprite = item.GetSprite;
        text.SetText("Lv " + item.Level);
        gameObject.SetActive(true);
    }

    public void SetGoods(ClientEnum.Goods target,int vlaue)
    {
        goods = target;
        num = vlaue;
        image.sprite = ResourcesManager.Instance.GetGoodsSprite(goods);
        text.SetText(num);
        gameObject.SetActive(true); 
    }

    public void SetTypeItem(ClientEnum.Reward reward,int index,int value)
    {
        switch (reward)
        {
            case ClientEnum.Reward.Item:

                break;
            case ClientEnum.Reward.Goods:
                SetGoods((ClientEnum.Goods)index, value);
;                break;
            default:
                break;
        }
    }
}
