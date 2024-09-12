using ClientEnum;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class DrawPanel : BasePanel
{
    [SerializeField, ReadOnly] Text title;
    [SerializeField, ReadOnly] Text currentDrawTitle;
    [SerializeField, ReadOnly] Text currentDrawDesc;
    [SerializeField, ReadOnly] Text currentDrawLimit;
    [SerializeField, ReadOnly] UIBuyButton oneDraw;
    [SerializeField, ReadOnly] UIBuyButton tenDraw;
    [SerializeField, ReadOnly] List<UIDrawSlot> slots = new List<UIDrawSlot>();
    [SerializeField, ReadOnly] UIDrawSlot prefab;
    [SerializeField, ReadOnly] RectTransform content;
    [SerializeField, ReadOnly] GameObject prevButton;
    [SerializeField, ReadOnly] GameObject nextButton;
    [SerializeField, ReadOnly] string drawLocal;

    UIDrawSlot currentSlot;
    bool CheckButton(DrawScriptable.Data draw , UIBuyButton button) => DataManager.Instance.CheckGoods(draw.Goods, draw.NeedValue * button.targetNum) && draw.Limit == 0 ? true : draw.Limit > 0;

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

        currentSlot = slots[0];
        oneDraw.title.text = string.Format(TableManager.Instance.TextScriptable.Get(drawLocal), oneDraw.targetNum);
        tenDraw.title.text = string.Format(TableManager.Instance.TextScriptable.Get(drawLocal), tenDraw.targetNum);
    }

    public override void OnUpdate()
    {
        UpdateDraw(currentSlot.GetCurrentData);
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
        currentSlot.ResetIndex();
        content.anchoredPosition = Vector2.zero;
        title.text = currentSlot.Title;

        SetDraw(currentSlot.GetCurrentData);
    }

    public void SetDraw(DrawScriptable.Data draw)
    {
        currentDrawDesc.text = TableManager.Instance.TextScriptable.Get(draw.DescKey);
        currentDrawTitle.text = TableManager.Instance.TextScriptable.Get(draw.NameKey);
        currentDrawLimit.gameObject.SetActive(draw.Limit > 0);

        prevButton.SetActive(!currentSlot.isMin);
        nextButton.SetActive(!currentSlot.isMax);

        UpdateDraw(draw);
    }

    void UpdateDraw(DrawScriptable.Data draw)
    {
        currentDrawLimit.text = string.Format("{0}/{1}", 0, draw.Limit);
        oneDraw.button.interactable = CheckButton(draw, oneDraw);
        tenDraw.button.interactable = CheckButton(draw, tenDraw);
    }

    public void OnClickDraw(UIBuyButton buyButton)
    {
        DataManager.Instance.UseGoods(currentSlot.GetCurrentData.Goods, currentSlot.GetCurrentData.NeedValue * buyButton.targetNum);
        UpdateDraw(currentSlot.GetCurrentData);
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
