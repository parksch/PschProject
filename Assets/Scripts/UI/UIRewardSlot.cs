using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRewardSlot : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Text text;
    ClientEnum.Goods goods;
    int num;

    public bool CheckGoods(ClientEnum.Goods target,int value)
    {
        if (target == goods)
        {
            num += value;
            return true;
        }

        return false;
    }

    public void SetItem(BaseItem item)
    {
        goods = ClientEnum.Goods.None;
        image.sprite = item.GetSprite;
        text.text = "Lv " + item.Level;
        gameObject.SetActive(true);
    }

    public void SetGoods(ClientEnum.Goods target,int vlaue)
    {
        goods = target;
        num = vlaue;
        image.sprite = ResourcesManager.Instance.GetGoodsSprite(goods);
        gameObject.SetActive(true); 
    }
}
