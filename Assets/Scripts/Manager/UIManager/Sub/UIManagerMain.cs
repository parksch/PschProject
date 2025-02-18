using JsonClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField, ReadOnly] UIGuide uIGuide;
    [SerializeField, ReadOnly] RectTransform buffParent;
    [SerializeField, ReadOnly] WorldCanvas worldCanvas;
    [SerializeField, ReadOnly] GameObject bossButton;
    [SerializeField, ReadOnly] GameObject escapeButton;

    public UIGuide Guide => uIGuide;
    public WorldCanvas WorldCanvas => worldCanvas;

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

        if (buffButtonText.text == "Close")
        {
            buffButtonText.text = "Open";
            buffSlots.gameObject.SetActive(false);
        }
        else
        {
            buffButtonText.text = "Close";
            buffSlots.CheckBuff();
            buffSlots.gameObject.SetActive(true);
        }
    }

    public void OffEscape()
    {
        escapeButton.SetActive(false);
    }

    public void UpdateBossHp(UNBigStats hp)
    {
        bossHP.UpdateHp(hp);
    }
    public void AddBuff(BuffBase buff, float timer, float value)
    {
        buffSlots.AddBuff(buff, value, timer);
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
    public void SetBossUI(UNBigStats hp,string name)
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
                bossButton.gameObject.SetActive(DataManager.Instance.ChallengingStage != 0);
                bossHP.gameObject.SetActive(false);
                escapeButton.gameObject.SetActive(false);
                break;
            case ClientEnum.GameMode.Boss:
            case ClientEnum.GameMode.GoldDungeon:
            case ClientEnum.GameMode.GemDungeon:
                bossButton.gameObject.SetActive(false);
                bossHP.gameObject.SetActive(true);
                escapeButton.gameObject.SetActive(true);
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

        uIGuide.Init();
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
