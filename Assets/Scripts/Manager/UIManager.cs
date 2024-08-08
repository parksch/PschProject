using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField, ReadOnly] UIText goldText;
    [SerializeField, ReadOnly] UIText rubyText;
    [SerializeField, ReadOnly] Text userName;
    [SerializeField, ReadOnly] Text level;
    [SerializeField, ReadOnly] Slider hpSlider;
    [SerializeField, ReadOnly] Slider expSlider;
    [SerializeField, ReadOnly] BasePanel currentPanel;
    [SerializeField, ReadOnly] UIBossHP bossHP;
    [SerializeField, ReadOnly] UIUserInfo userInfo;
    [SerializeField] List<BasePanel> panels;

    Stack<BasePanel> panelStack = new Stack<BasePanel>();

    public delegate void ChangeHP(float ratio);
    public ChangeHP OnChangeHP;


    protected override void Awake()
    {

    }

    public void Init()
    {
        SetGold(DataManager.Instance.GetGoods.gold);
        SetRuby(DataManager.Instance.GetGoods.ruby);
        userInfo.Init();

        for (int i = 0; i < panels.Count; i++)
        {
            panels[i].FirstLoad();
        }

        OnChangeHP += userInfo.SetHP;

        DataManager.Instance.OnChangeGold += SetGold;

        DataManager.Instance.OnChangeExp += userInfo.SetExp;
    }

    public void SetGold(long gold)
    {
        goldText.SetText(gold);
    }

    public void SetRuby(long ruby) 
    {
        rubyText.SetText(ruby);
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
    }

    public T GetPanel<T>() where T : BasePanel
    {
        return panels.Find(x => x.GetType() == typeof(T)) as T;
    }
}
