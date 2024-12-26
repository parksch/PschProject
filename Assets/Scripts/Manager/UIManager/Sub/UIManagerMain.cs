using JsonClass;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public partial class UIManager //Main
{
    [SerializeField, ReadOnly] List<UIActiveSkillSlot> slots;
    [SerializeField, ReadOnly] UIBuffSlots buffSlots;
    [SerializeField, ReadOnly] UIToggle autoSkill;
    [SerializeField, ReadOnly] UIText goldText;
    [SerializeField, ReadOnly] UIText gemText;
    [SerializeField, ReadOnly] UIText scrapText;
    [SerializeField, ReadOnly] UIText reinforceText;
    [SerializeField, ReadOnly] UIText amplificationText;
    [SerializeField, ReadOnly] UIText goldDungeonTicket;
    [SerializeField, ReadOnly] UIText gemDungeonTicket;
    [SerializeField, ReadOnly] Text buffButtonText;
    [SerializeField, ReadOnly] Text stageText;
    [SerializeField, ReadOnly] Text userName;
    [SerializeField, ReadOnly] Text level;
    [SerializeField, ReadOnly] UIBossHP bossHP;
    [SerializeField, ReadOnly] UIUserInfo userInfo;
    [SerializeField, ReadOnly] RectTransform buffParent;
    [SerializeField, ReadOnly] GameObject bossButton;

    public delegate void ChangeHP(float ratio);

    public ChangeHP OnChangePlayerHP;

    public bool IsAutoSkill => autoSkill.IsOn;
    public List<UIActiveSkillSlot> SkillSlots => slots;

    public void OnClickBossChallenge()
    {
        GameManager.Instance.OnChangeGameMode(ClientEnum.GameMode.Boss);
    }
    public void OnClickBuff()
    {
        RectTransform rectTransform = buffSlots.GetComponent<RectTransform>();

        if (buffButtonText.text == "Close")
        {
            buffButtonText.text = "Open";
            rectTransform.offsetMax = new Vector2(0, -buffParent.GetComponent<RectTransform>().rect.height);
        }
        else
        {
            buffButtonText.text = "Close";
            rectTransform.offsetMax = new Vector2(0, -50);
        }
    }

    public void UpdateBossHp(long hp)
    {
        bossHP.UpdateHp(hp);
    }
    public void AddBuff(BuffData buffData, float timer, float value)
    {
        buffSlots.AddBuff(buffData, value, timer);
    }

    public void ResetSkill()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].ResetSkill();
        }
    }

    public void ResetBuff()
    {
        buffSlots.ResetBuffs();
    }

    public void SetStageTitle(string local,int stageNum)
    {
        string title = ScriptableManager.Instance.Get<LocalizationScriptable>(ScriptableType.Localization).Get(local);
        stageText.text = string.Format(title, stageNum);
    }
    public void SetBossUI(long hp,string name)
    {
        bossHP.SetHP(hp);
        bossHP.SetName(name);
        bossHP.SetTime(ScriptableManager.Instance.Get<DefaultValuesScriptable>(ScriptableType.DefaultValues).Get("TimeLimit"));
        bossHP.SetOn();
    }

    void SetGoodsText(ClientEnum.Goods type,long value)
    {
        switch (type)
        {
            case ClientEnum.Goods.Gold:
                goldText.SetText(value);
                break;
            case ClientEnum.Goods.Scrap:
                scrapText.SetText(value);
                break;
            case ClientEnum.Goods.Gem:
                gemText.SetText(value);
                break;
            case ClientEnum.Goods.Reinforce:
                reinforceText.SetText(value);
                break;
            case ClientEnum.Goods.Amplification:
                amplificationText.SetText(value);
                break;
            case ClientEnum.Goods.GoldDungeonTicket:
                goldDungeonTicket.SetText(value);
                break;
            case ClientEnum.Goods.GemDungeonTicket:
                gemDungeonTicket.SetText(value);
                break;
            case ClientEnum.Goods.Money:
                break;
            default:
                break;
        }
    }

    void SetGameModeUI(ClientEnum.GameMode gameMode)
    {
        switch (gameMode)
        {
            case ClientEnum.GameMode.Stage:
                bossButton.gameObject.SetActive(DataManager.Instance.GetInfo.ChallengingStage != 0);
                bossHP.gameObject.SetActive(false);
                break;
            case ClientEnum.GameMode.Boss:
                bossButton.gameObject.SetActive(false);
                bossHP.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    void InitTopUI()
    {
        GameManager.Instance.OnChangeGameMode += SetGameModeUI;

        for (int i = (int)ClientEnum.Goods.Gold; i < (int)ClientEnum.Goods.Max; i++)
        {
            ClientEnum.Goods goods = (ClientEnum.Goods)i;
            SetGoodsText(goods, DataManager.Instance.GetGoods(goods));
        }

        userInfo.Init();
        OnChangePlayerHP += userInfo.SetHP;

        DataManager.Instance.OnChangeGoods += (type,value) => { SetGoodsText(type, value); };
        DataManager.Instance.OnChangeGoods += (_,_) => { UpdatePanel(); };

        DataManager.Instance.OnChangeExp += _ => { UpdatePanel(); };

        DataManager.Instance.OnChangeSkill += (_,_)=> 
        {
            for (int i = 0; i < slots.Count; i++)
            {
                slots[i].SetSkill(DataManager.Instance.EquipSkill[i]);
            }
        };
    }

}
