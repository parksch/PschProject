using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField,ReadOnly] UIText goldText;
    [SerializeField,ReadOnly] UIText rubyText;
    [SerializeField,ReadOnly] Text userName;
    [SerializeField,ReadOnly] Text level;
    [SerializeField] Slider hpSlider;
    [SerializeField] Slider expSlider;

    public delegate void ChangeHP(long curHp);

    public ChangeHP OnChangeHP;

    protected override void Awake()
    {
    }

    public void Init()
    {
        SetGold(DataManager.Instance.GetGoods.gold);
        SetRuby(DataManager.Instance.GetGoods.ruby);

        OnChangeHP = (value) => 
        { 
        };

    }

    public void SetGold(long gold)
    {
        goldText.SetText(gold);
    }

    public void SetRuby(long ruby) 
    {
        rubyText.SetText(ruby);
    }

}
