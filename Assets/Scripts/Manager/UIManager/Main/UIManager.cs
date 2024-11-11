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

        foreach (var item in panelStack)
        {
            if (item.gameObject.activeSelf)
            {
                item.OnUpdate();
            }
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

        while (panelStack.Count > 0)
        {
            panelStack.Pop().Close();
        }

        OpenPaenl(menuButton.targetPanel);
    }

    public void OpenPaenl(BasePanel panel)
    {
        if (currentPanel != null)
        {
            if (panel == null || !panel.IsTranslucent)
            {
                ClosePaenl();
            }
        }

        currentPanel = panel;

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
        bool isTranslucent = currentPanel.IsTranslucent;

        ClosePaenl();

        if (panels.Count > 0)
        {
            if (isTranslucent)
            {
                currentPanel = panelStack.Pop();
                ResetTop();
                OpenTop(currentPanel.OpenMenu);
            }
            else
            {
                OpenPaenl(panelStack.Pop());
            }
        }
    }

    public T Get<T>() where T : BasePanel
    {
        return panels.Find(x => x.GetType() == typeof(T)) as T;
    }
}
