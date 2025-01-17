using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JsonClass;

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
    bool CheckButton(Shops shop, UIBuyButton button)
    {
        if (shop.limit == 0)
        {
            return DataManager.Instance.CheckGoods(shop.Goods(), shop.needValue * button.targetNum);
        }
        else
        {
            if (shop.limit >= DataManager.Instance.GetInfo.DrawLimit(shop.nameKey) + button.targetNum)
            {
                return DataManager.Instance.CheckGoods(shop.Goods(), shop.needValue * button.targetNum);
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
            JsonClass.Draw draw = ScriptableManager.Instance.Get<JsonClass.DrawScriptable>(ScriptableType.Draw).GetData(i);

            if (draw == null)
            {
                continue;
            }

            if (i == ClientEnum.Draw.Min + 1)
            {
                prefab.Set(i,draw.type.titleKey, draw.type.shops);
            }
            else
            {
                UIDrawSlot slot = Instantiate(prefab, prefab.transform.parent).GetComponent<UIDrawSlot>();
                slot.Set(i,draw.type.titleKey, draw.type.shops);
                slots.Add(slot);
            }
        }

        currentSlot = slots[0];
        oneDraw.title.text = string.Format(ScriptableManager.Instance.Get<LocalizationScriptable>(ScriptableType.Localization).Get(drawLocal), oneDraw.targetNum);
        tenDraw.title.text = string.Format(ScriptableManager.Instance.Get<LocalizationScriptable>(ScriptableType.Localization).Get(drawLocal), tenDraw.targetNum);
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
        base.Open();
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

    public void SetDraw(Shops shop)
    {
        currentDrawDesc.text = ScriptableManager.Instance.Get<LocalizationScriptable>(ScriptableType.Localization).Get(shop.descKey);
        currentDrawTitle.text = ScriptableManager.Instance.Get<LocalizationScriptable>(ScriptableType.Localization).Get(shop.nameKey);
        currentDrawLimit.gameObject.SetActive(shop.limit> 0);
        lvSlider.gameObject.SetActive(shop.maxLevel > 0);

        prevButton.SetActive(!currentSlot.isMin);
        nextButton.SetActive(!currentSlot.isMax);

        UpdateDraw(shop);
    }

    void UpdateDraw(Shops shop)
    {
        if (shop.limit > 0)
        {
            currentDrawLimit.text = string.Format("{0}/{1}", DataManager.Instance.GetInfo.DrawLimit(shop.nameKey), shop.limit);
        }
        if (shop.maxLevel > 0)
        {
            int drawCount = DataManager.Instance.GetInfo.DrawCount(shop.nameKey);
            int requiredExp = ScriptableManager.Instance.Get<DrawScriptable>(ScriptableType.Draw).RequiredExp;

            if (drawCount < shop.maxLevel * requiredExp)
            {
                lvText.text = string.Format("Lv {0} {1}/{2}", (drawCount / requiredExp), drawCount % requiredExp, requiredExp);
            }
            else
            {
                lvText.text = string.Format("Lv {0}", (drawCount / requiredExp));
            }
            lvSlider.value = (float)(drawCount % requiredExp) / requiredExp;
        }

        oneDraw.button.interactable = CheckButton(shop, oneDraw);
        tenDraw.button.interactable = CheckButton(shop, tenDraw);
    }

    public void OnClickDraw(UIBuyButton buyButton)
    {
        if (currentSlot.GetCurrentData.limit > 0)
        {
            DataManager.Instance.GetInfo.AddDrawLimit(currentSlot.GetCurrentData.nameKey, buyButton.targetNum);
        }

        if (currentSlot.GetCurrentData.maxLevel > 0)
        {
            int maxCount = currentSlot.GetCurrentData.maxLevel * ScriptableManager.Instance.Get<DrawScriptable>(ScriptableType.Draw).RequiredExp;
            int currentCount = DataManager.Instance.GetInfo.DrawCount(currentSlot.GetCurrentData.nameKey);

            if (maxCount > currentCount)
            {
                if (maxCount < currentCount + buyButton.targetNum)
                {
                    int addCount = buyButton.targetNum - ( (currentCount + buyButton.targetNum) - maxCount);
                    DataManager.Instance.GetInfo.AddDrawCount(currentSlot.GetCurrentData.nameKey, addCount);
                }
                else
                {
                    DataManager.Instance.GetInfo.AddDrawCount(currentSlot.GetCurrentData.nameKey, buyButton.targetNum);
                }
            }
        }

        DataManager.Instance.UseGoods(currentSlot.GetCurrentData.Goods(), currentSlot.GetCurrentData.needValue * buyButton.targetNum);
        GetResult(currentSlot.GetCurrentData,buyButton.targetNum);
        UpdateDraw(currentSlot.GetCurrentData);
    }

    public void GetResult(Shops shop, int num)
    {
        RewardPanel reward = UIManager.Instance.Get<RewardPanel>();
        reward.CopyTopMenu(activeTopUI);

        switch (shop.DrawValue())
        {
            case ClientEnum.DrawValue.Item:
                Item(reward, shop, num);
                break;
            case ClientEnum.DrawValue.Skill:
                Skill(reward, shop, num);
                break;
            default:
                break;
        }

        UIManager.Instance.AddPanel(reward);
    }

    void Skill(RewardPanel reward, Shops shop,int num)
    {
        List<(SkillData skill, int piece)> data = new List<(SkillData, int)>();

        for (int i = 0; i < num; i++)
        {
            ClientEnum.Grade grade = shop.Grade();
            SkillData skillData = ScriptableManager.Instance.Get<SkillDataScriptable>(ScriptableType.SkillData).GetDataInGrade(grade);


        }
    }

    void Item(RewardPanel reward, Shops shop, int num)
    {
        for (int i = 0; i < num; i++)
        {
            ClientEnum.Item target = shop.Target();

            if (target == ClientEnum.Item.None)
            {
                target = ScriptableManager.Instance.Get<ItemDataScriptable>(ScriptableType.ItemData).GetRandomTarget();
            }

            Items info = ScriptableManager.Instance.Get<ItemDataScriptable>(ScriptableType.ItemData).GetRandom(target);

            ClientEnum.Grade grade = shop.Grade();

            BaseItem item = ItemFactory.Create(target);
            item.Set(info, grade);
            reward.AddItem(item);
            DataManager.Instance.AddItem(item);
        }
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
