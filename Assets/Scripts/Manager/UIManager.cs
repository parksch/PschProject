using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField, ReadOnly] UIText goldText;
    [SerializeField, ReadOnly] UIText gemText;
    [SerializeField, ReadOnly] UIText scrapText;
    [SerializeField, ReadOnly] Text userName;
    [SerializeField, ReadOnly] Text level;
    [SerializeField, ReadOnly] Slider hpSlider;
    [SerializeField, ReadOnly] Slider expSlider;
    [SerializeField, ReadOnly] BasePanel currentPanel;
    [SerializeField, ReadOnly] UIBossHP bossHP;
    [SerializeField, ReadOnly] UIUserInfo userInfo;
    [SerializeField, ReadOnly] RewardPanel rewardPanel;
    [SerializeField, ReadOnly] List<GameObject> mainTop = new List<GameObject>();
    [SerializeField] List<GameObject> topMenu = new List<GameObject>();
    [SerializeField] List<BasePanel> panels;

    Stack<BasePanel> panelStack = new Stack<BasePanel>();

    public delegate void ChangeHP(float ratio);
    public ChangeHP OnChangeHP;

    public void OpenTop(List<GameObject> target)
    {
        for (int i = 0; i < target.Count; i++)
        {
            target[i].SetActive(true);
        }
    }

    protected override void Awake()
    {

    }

    public void UpdatePanel()
    {
        if (currentPanel != null)
        {
            currentPanel.OnUpdate();
        }
    }   

    public void Init()
    {
        SetGold(DataManager.Instance.GetGoods.gold);
        SetGem(DataManager.Instance.GetGoods.gem);
        SetScrap(DataManager.Instance.GetGoods.scrap);

        userInfo.Init();

        for (int i = 0; i < panels.Count; i++)
        {
            panels[i].FirstLoad();
        }

        OnChangeHP += userInfo.SetHP;

        DataManager.Instance.OnChangeGold += SetGold;
        DataManager.Instance.OnChangeGold += (value) => { UpdatePanel(); };
        DataManager.Instance.OnChangeScrap += SetScrap;
        DataManager.Instance.OnChangeScrap += (value) => { UpdatePanel(); };
        DataManager.Instance.OnChangeGem += SetGem;
        DataManager.Instance.OnChangeGem += (value) => { UpdatePanel(); };
        DataManager.Instance.OnChangeExp += userInfo.SetExp;
        DataManager.Instance.OnChangeExp += (value) => { UpdatePanel(); };

        for (int i = 0; i < topMenu.Count; i++)
        {
            topMenu[i].SetActive(false);
        }

        OpenTop(mainTop);
    }

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

    public void OnClickMenuButton(UIMenuButton menuButton)
    {
        if (menuButton.targetPanel == currentPanel)
        {
            return;
        }

        OpenPaenl(menuButton.targetPanel);
    }

    public void OpenPaenl(BasePanel paenl)
    {
        for (int i = 0; i < topMenu.Count; i++)
        {
            topMenu[i].SetActive(false);
        }

        if (currentPanel != null)
        {
            ClosePaenl();
        }

        currentPanel = paenl;

        if (currentPanel != null)
        {
            currentPanel.Open();
            currentPanel.gameObject.SetActive(true);
        }
        else if (currentPanel == null)
        {
            OpenTop(mainTop);
        }
    }

    public void ClosePaenl()
    {
        currentPanel.Close();
        currentPanel.gameObject.SetActive(false);
        currentPanel = null;
    }

    public void AddPanel(BasePanel panel)
    {
        if (currentPanel != null)
        {
            panelStack.Push(currentPanel);
        }

        OpenPaenl(panel);
    }

    public void BackPanel()
    {
        ClosePaenl();

        if (panels.Count > 0)
        {
            OpenPaenl(panelStack.Pop());
        }
        else
        {
            for (int i = 0; i < topMenu.Count; i++)
            {
                topMenu[i].SetActive(false);
            }

            OpenTop(mainTop);
        }
    }

    public T GetPanel<T>() where T : BasePanel
    {
        return panels.Find(x => x.GetType() == typeof(T)) as T;
    }

    public void AddRewardItem(ClientEnum.Goods goods, string value)
    {

    }

    public void AddRewardItem(ClientEnum.Item goods, string value)
    {

    }

    public void OpenRewardPanel()
    {
        rewardPanel.Open();
    }

    public void AddRewardPanel(BaseItem item)
    {
        rewardPanel.AddItem(item);
    }
}
