using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class ShopPanel : BasePanel
{
    [SerializeField,ReadOnly] Text title;
    [SerializeField,ReadOnly] UIBuyButton oneDraw;
    [SerializeField,ReadOnly] UIBuyButton tenDraw;
    [SerializeField,ReadOnly] string drawLocal;
    [SerializeField,ReadOnly] List<UIShopSlot> slots = new List<UIShopSlot>();
    [SerializeField,ReadOnly] UIShopSlot prefab;
    [SerializeField,ReadOnly] RectTransform content;
    UIShopSlot currentSlot;


    public override void FirstLoad()
    {
        for (var i = ClientEnum.Shop.Start; i < ClientEnum.Shop.End; i++)
        {
            ShopScriptable.Category shop = TableManager.Instance.ShopScriptable.GetData(i);

            if (shop == null)
            {
                continue;
            }

            if (i == ClientEnum.Shop.Start + 1)
            {
                prefab.Set(i,shop.NameStringKey, shop.Datas);
            }
            else
            {
                UIShopSlot slot = Instantiate(prefab, prefab.transform.parent).GetComponent<UIShopSlot>();
                slot.Set(i,shop.NameStringKey, shop.Datas);
                slots.Add(slot);
            }
        }

        currentSlot = slots[0];
        oneDraw.title.text = string.Format(TableManager.Instance.TextScriptable.Get(drawLocal), oneDraw.targetNum);
        tenDraw.title.text = string.Format(TableManager.Instance.TextScriptable.Get(drawLocal), tenDraw.targetNum);
    }

    public override void Close()
    {
    }

    public override void Open()
    {
        title.text = currentSlot.Title;

    }

    public void Set()
    {

    }

    public void OnClickDraw(UIBuyButton buyButton)
    {

    }
}
