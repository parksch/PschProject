using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class UIManager //Top
{
    [SerializeField, ReadOnly] UIText goldText;
    [SerializeField, ReadOnly] UIText gemText;
    [SerializeField, ReadOnly] UIText scrapText;
    [SerializeField] UIText reinforceText;
    [SerializeField, ReadOnly] Text userName;
    [SerializeField, ReadOnly] Text level;
    [SerializeField, ReadOnly] Slider hpSlider;
    [SerializeField, ReadOnly] Slider expSlider;
    [SerializeField, ReadOnly] UIBossHP bossHP;
    [SerializeField, ReadOnly] UIUserInfo userInfo;

    public void SetGold(long gold)
    {
        goldText.SetText(gold);
    }

    public void SetGem(long ruby)
    {
        gemText.SetText(ruby);
    }

    public void SetScrap(long scrap)
    {
        scrapText.SetText(scrap);
    }

    void InitTopUI()
    {
        SetGold(DataManager.Instance.GetGoods.gold);
        SetGem(DataManager.Instance.GetGoods.gem);
        SetScrap(DataManager.Instance.GetGoods.scrap);

        userInfo.Init();

        OnChangeHP += userInfo.SetHP;

        DataManager.Instance.OnChangeGold += SetGold;
        DataManager.Instance.OnChangeGold += (value) => { UpdatePanel(); };
        DataManager.Instance.OnChangeScrap += SetScrap;
        DataManager.Instance.OnChangeScrap += (value) => { UpdatePanel(); };
        DataManager.Instance.OnChangeGem += SetGem;
        DataManager.Instance.OnChangeGem += (value) => { UpdatePanel(); };
        DataManager.Instance.OnChangeExp += userInfo.SetExp;
        DataManager.Instance.OnChangeExp += (value) => { UpdatePanel(); };
    }
}
