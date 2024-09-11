using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class DrawPanel : BasePanel
{
    [SerializeField,ReadOnly] Text title;
    [SerializeField,ReadOnly] UIBuyButton oneDraw;
    [SerializeField,ReadOnly] UIBuyButton tenDraw;
    [SerializeField,ReadOnly] string drawLocal;
    [SerializeField,ReadOnly] List<UIDrawSlot> slots = new List<UIDrawSlot>();
    [SerializeField,ReadOnly] UIDrawSlot prefab;
    [SerializeField,ReadOnly] RectTransform content;
    [SerializeField] GameObject prevButton;
    [SerializeField] GameObject nextButton;

    [SerializeField] Text currentDrawTitle;
    [SerializeField] Text currentDrawDesc;

    UIDrawSlot currentSlot;

    public override void FirstLoad()
    {
        for (var i = ClientEnum.Draw.Min; i < ClientEnum.Draw.Max; i++)
        {
            DrawScriptable.Category shop = TableManager.Instance.ShopScriptable.GetData(i);

            if (shop == null)
            {
                continue;
            }

            if (i == ClientEnum.Draw.Min + 1)
            {
                prefab.Set(i,shop.NameStringKey, shop.Datas);
            }
            else
            {
                UIDrawSlot slot = Instantiate(prefab, prefab.transform.parent).GetComponent<UIDrawSlot>();
                slot.Set(i,shop.NameStringKey, shop.Datas);
                slots.Add(slot);
            }
        }

        oneDraw.title.text = string.Format(TableManager.Instance.TextScriptable.Get(drawLocal), oneDraw.targetNum);
        tenDraw.title.text = string.Format(TableManager.Instance.TextScriptable.Get(drawLocal), tenDraw.targetNum);
    }

    public override void Close()
    {

    }

    public override void Open()
    {
        SetSlot(slots[0]);
    }

    public void SetSlot(UIDrawSlot slot)
    {
        currentSlot = slot;
        content.anchoredPosition = Vector2.zero;
        title.text = currentSlot.Title;

        SetDraw(currentSlot.GetCurrentData);
    }

    public void SetDraw(DrawScriptable.Data draw)
    {


        prevButton.SetActive(!currentSlot.isMin);
        nextButton.SetActive(!currentSlot.isMax);
    }

    public void OnClickDraw(UIBuyButton buyButton)
    {

    }

    public void OnClickSlot(UIDrawSlot uIShopSlot)
    {
        SetSlot(uIShopSlot);
    }

    public void OnClickPrev()
    {
        currentSlot.SubtractIndex();
        SetDraw(currentSlot.GetCurrentData);
    }

    public void OnClickNext()
    {
        currentSlot.AddIndex();
        SetDraw(currentSlot.GetCurrentData);
    }
}
