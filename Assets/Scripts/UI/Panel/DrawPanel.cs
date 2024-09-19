using ClientEnum;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


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
    [SerializeField, ReadOnly] UnityEngine.UI.Slider lvSlider;
    [SerializeField, ReadOnly] Text lvText;
    [SerializeField, ReadOnly] string drawLocal;

    UIDrawSlot currentSlot;
    bool CheckButton(DrawScriptable.Data draw, UIBuyButton button)
    {
        if (draw.Limit == 0)
        {
            return DataManager.Instance.CheckGoods(draw.Goods, draw.NeedValue * button.targetNum);
        }
        else
        {
            if (draw.Limit >= DataManager.Instance.GetInfo.DrawLimit(draw.NameKey) + button.targetNum)
            {
                return DataManager.Instance.CheckGoods(draw.Goods, draw.NeedValue * button.targetNum);
            }
            else
            {
                return false;
            }
        }
    }

    public override void FirstLoad()
    {
        for (var i = ClientEnum.Draw.Min; i < ClientEnum.Draw.Max; i++)
        {
            DrawScriptable.Category shop = TableManager.Instance.DrawScriptable.GetData(i);

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
        lvSlider.gameObject.SetActive(draw.MaxLevel > 0);

        prevButton.SetActive(!currentSlot.isMin);
        nextButton.SetActive(!currentSlot.isMax);

        UpdateDraw(draw);
    }

    void UpdateDraw(DrawScriptable.Data draw)
    {
        if (draw.Limit > 0)
        {
            currentDrawLimit.text = string.Format("{0}/{1}", DataManager.Instance.GetInfo.DrawLimit(draw.NameKey), draw.Limit);
        }
        if (draw.MaxLevel > 0)
        {
            int drawCount = DataManager.Instance.GetInfo.DrawCount(draw.NameKey);
            int requiredExp = TableManager.Instance.DrawScriptable.RequiredExp;

            if (drawCount < draw.MaxLevel * requiredExp)
            {
                lvText.text = string.Format("Lv {0} {1}/{2}", (drawCount / requiredExp), drawCount % requiredExp, requiredExp);
            }
            else
            {
                lvText.text = string.Format("Lv {0}", (drawCount / requiredExp));
            }
            lvSlider.value = (float)(drawCount % requiredExp) / requiredExp;
        }

        oneDraw.button.interactable = CheckButton(draw, oneDraw);
        tenDraw.button.interactable = CheckButton(draw, tenDraw);
    }

    public void OnClickDraw(UIBuyButton buyButton)
    {
        if (currentSlot.GetCurrentData.Limit > 0)
        {
            DataManager.Instance.GetInfo.AddDrawLimit(currentSlot.GetCurrentData.NameKey, buyButton.targetNum);
        }

        if (currentSlot.GetCurrentData.MaxLevel > 0)
        {
            int maxCount = currentSlot.GetCurrentData.MaxLevel * TableManager.Instance.DrawScriptable.RequiredExp;
            int currentCount = DataManager.Instance.GetInfo.DrawCount(currentSlot.GetCurrentData.NameKey);

            if (maxCount > currentCount)
            {
                if (maxCount < currentCount + buyButton.targetNum)
                {
                    int addCount = buyButton.targetNum - ( (currentCount + buyButton.targetNum) - maxCount);
                    DataManager.Instance.GetInfo.AddDrawCount(currentSlot.GetCurrentData.NameKey, addCount);
                }
                else
                {
                    DataManager.Instance.GetInfo.AddDrawCount(currentSlot.GetCurrentData.NameKey, buyButton.targetNum);
                }
            }
        }

        DataManager.Instance.UseGoods(currentSlot.GetCurrentData.Goods, currentSlot.GetCurrentData.NeedValue * buyButton.targetNum);
        GetItem(currentSlot.GetCurrentData,buyButton.targetNum);
        UpdateDraw(currentSlot.GetCurrentData);
    }

    public void GetItem(DrawScriptable.Data draw, int num)
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
