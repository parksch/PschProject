using JsonClass;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public partial class UIManager //Top
{
    [SerializeField, ReadOnly] UIText goldText;
    [SerializeField, ReadOnly] UIText gemText;
    [SerializeField, ReadOnly] UIText scrapText;
    [SerializeField, ReadOnly] UIText reinforceText;
    [SerializeField, ReadOnly] Text stageText;
    [SerializeField, ReadOnly] Text userName;
    [SerializeField, ReadOnly] Text level;
    [SerializeField, ReadOnly] Slider hpSlider;
    [SerializeField, ReadOnly] Slider expSlider;
    [SerializeField, ReadOnly] UIBossHP bossHP;
    [SerializeField, ReadOnly] UIUserInfo userInfo;
    [SerializeField] GameObject bossButton;

    public delegate void ChangeHP(float ratio);
    public ChangeHP OnChangePlayerHP;

    public void SetStageTitle(string local,int stageNum)
    {
        string title = ScriptableManager.Instance.Get<LocalizationScriptable>(ScriptableType.Localization).Get(local);
        stageText.text = string.Format(title, stageNum);
    }

    public void OnClickBossChallenge()
    {
        GameManager.Instance.OnChangeGameMode(ClientEnum.GameMode.Boss);
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

    void SetGameModeUI(ClientEnum.GameMode gameMode)
    {
        switch (gameMode)
        {
            case ClientEnum.GameMode.Stage:
                bossButton.gameObject.SetActive(true);
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
        SetReinforce(DataManager.Instance.GetGoods.reinforceStone);

        userInfo.Init();

        OnChangePlayerHP += userInfo.SetHP;

        DataManager.Instance.OnChangeGold = null;
        DataManager.Instance.OnChangeGold += SetGold;
        DataManager.Instance.OnChangeGold += _ => { UpdatePanel();};

        DataManager.Instance.OnChangeScrap = null;
        DataManager.Instance.OnChangeScrap += SetScrap;
        DataManager.Instance.OnChangeScrap += _ => { UpdatePanel(); };

        DataManager.Instance.OnChangeGem = null;
        DataManager.Instance.OnChangeGem += SetGem;
        DataManager.Instance.OnChangeGem += _ => { UpdatePanel(); };

        DataManager.Instance.OnChangeReinforce = null;
        DataManager.Instance.OnChangeReinforce += SetReinforce;
        DataManager.Instance.OnChangeReinforce += _ => { UpdatePanel(); };

        DataManager.Instance.OnChangeExp = null;
        DataManager.Instance.OnChangeExp += userInfo.SetExp;
        DataManager.Instance.OnChangeExp += _ => { UpdatePanel(); };
    }

}
