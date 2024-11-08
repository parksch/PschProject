using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngineInternal;

public partial class UIManager : Singleton<UIManager>
{
    [SerializeField, ReadOnly] BasePanel currentPanel;
    [SerializeField, ReadOnly] List<GameObject> mainTop = new List<GameObject>();
    [SerializeField] List<GameObject> topMenu = new List<GameObject>();
    [SerializeField] List<BasePanel> panels;

    Stack<BasePanel> panelStack = new Stack<BasePanel>();

    public delegate void ChangeHP(float ratio);
    public ChangeHP OnChangeHP;

    protected override void Awake()
    {

    }

    public void OpenTop(List<GameObject> target)
    {
        for (int i = 0; i < target.Count; i++)
        {
            target[i].SetActive(true);
        }
    }

    public void ResetTop()
    {
        for (int i = 0; i < topMenu.Count; i++)
        {
            topMenu[i].SetActive(false);
        }
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
        InitTopUI();

        for (int i = 0; i < panels.Count; i++)
        {
            panels[i].FirstLoad();
        }

        for (int i = 0; i < topMenu.Count; i++)
        {
            topMenu[i].SetActive(false);
        }

        OpenTop(mainTop);
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
            ResetTop();
            currentPanel.Open();
            currentPanel.gameObject.SetActive(true);
        }
    }

    public void ClosePaenl()
    {
        currentPanel.Close();
        currentPanel.gameObject.SetActive(false);
        currentPanel = null;

        ResetTop();
        OpenTop(mainTop);
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
