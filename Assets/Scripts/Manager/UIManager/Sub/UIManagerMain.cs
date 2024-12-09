using JsonClass;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public partial class UIManager //Main
{
    [SerializeField, ReadOnly] List<UIActiveSkillSlot> slots;
    [SerializeField, ReadOnly] UIToggle autoSkill;
    [SerializeField, ReadOnly] UIText goldText;
    [SerializeField, ReadOnly] UIText gemText;
    [SerializeField, ReadOnly] UIText scrapText;
    [SerializeField, ReadOnly] UIText reinforceText;
    [SerializeField, ReadOnly] UIText amplificationText;
    [SerializeField, ReadOnly] Text stageText;
    [SerializeField, ReadOnly] Text userName;
    [SerializeField, ReadOnly] Text level;
    [SerializeField, ReadOnly] UIBossHP bossHP;
    [SerializeField, ReadOnly] UIUserInfo userInfo;
    [SerializeField, ReadOnly] GameObject bossButton;

    public delegate void ChangeHP(float ratio);

    public ChangeHP OnChangePlayerHP;

    public bool IsAutoSkill => autoSkill.IsOn;
    public List<UIActiveSkillSlot> SkillSlots => slots;

    public void OnClickBossChallenge()
    {
        GameManager.Instance.OnChangeGameMode(ClientEnum.GameMode.Boss);
    }
    public void UpdateBossHp(long hp)
    {
        bossHP.UpdateHp(hp);
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

    void SetGold(long gold)
    {
        goldText.SetText(gold);
    }
    void SetGem(long ruby)
    {
        gemText.SetText(ruby);
    }
    void SetScrap(long scrap)
    {
        scrapText.SetText(scrap);
    }
    void SetReinforce(long scrap)
    {
        reinforceText.SetText(scrap);
    }

    void SetAmplification(long amplification)
    {
        amplificationText.SetText(amplification);
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

        SetGold(DataManager.Instance.GetGoods.gold);
        SetGem(DataManager.Instance.GetGoods.gem);
        SetScrap(DataManager.Instance.GetGoods.scrap);
        SetReinforce(DataManager.Instance.GetGoods.reinforce);
        SetAmplification (DataManager.Instance.GetGoods.amplification);

        userInfo.Init();
        OnChangePlayerHP += userInfo.SetHP;

        DataManager.Instance.OnChangeGem += _ => { SetGem(DataManager.Instance.GetGoods.gem); };
        DataManager.Instance.OnChangeGem += _ => { UpdatePanel(); };

        DataManager.Instance.OnChangeGold += _ => { SetGold(DataManager.Instance.GetGoods.gold); };
        DataManager.Instance.OnChangeGold += _ => { UpdatePanel();};

        DataManager.Instance.OnChangeScrap += _ => { SetScrap(DataManager.Instance.GetGoods.scrap); };
        DataManager.Instance.OnChangeScrap += _ => { UpdatePanel(); };

        DataManager.Instance.OnChangeReinforce += _ => { SetReinforce(DataManager.Instance.GetGoods.reinforce); };
        DataManager.Instance.OnChangeReinforce += _ => { UpdatePanel(); };

        DataManager.Instance.OnChangeAmplification += _ => { SetAmplification(DataManager.Instance.GetGoods.amplification); };
        DataManager.Instance.OnChangeAmplification += _ => { UpdatePanel(); };

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
