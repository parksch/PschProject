using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShopPanel : BasePanel
{
    [SerializeField,ReadOnly] Text title;
    [SerializeField,ReadOnly] UIBuyButton oneDraw;
    [SerializeField,ReadOnly] UIBuyButton tenDraw;
    [SerializeField,ReadOnly] string drawLocal;
    [SerializeField,ReadOnly] List<UIShopSlot> slots = new List<UIShopSlot>();
    [SerializeField] UIShopSlot prefab;

    public override void FirstLoad()
    {
        for (var i = ClientEnum.Shop.Start; i < ClientEnum.Shop.End; i++)
        {
            if (i == ClientEnum.Shop.Start)
            {
            }
            else
            {
                UIShopSlot slot = Instantiate(prefab, prefab.transform.parent).GetComponent<UIShopSlot>();
                slots.Add(slot);
            }
        }

        oneDraw.title.text = string.Format(TableManager.Instance.TextScriptable.GetText(drawLocal), oneDraw.targetNum);
        tenDraw.title.text = string.Format(TableManager.Instance.TextScriptable.GetText(drawLocal), tenDraw.targetNum);
    }

    public override void Close()
    {

    }

    public override void Open()
    {

    }

    public void Set()
    {

    }

    public void OnClickDraw(UIBuyButton buyButton)
    {

    }
}
