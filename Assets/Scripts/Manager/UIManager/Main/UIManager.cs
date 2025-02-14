using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class UIManager : Singleton<UIManager>
{
    [SerializeField, ReadOnly] GameObject mainScreen;
    [SerializeField, ReadOnly] BasePanel currentPanel;
    [SerializeField, ReadOnly] List<GameObject> mainTop = new List<GameObject>();
    [SerializeField] List<GameObject> topMenu = new List<GameObject>();
    [SerializeField] List<BasePanel> panels;

    public BasePanel CurrentPanel => currentPanel;

    public bool ContainsPanel(BasePanel target)
    {
        return panelStack.Contains(target);
    }

    Stack<BasePanel> panelStack = new Stack<BasePanel>();

    protected override void Awake()
    {

    }

    public void ActiveTop(List<GameObject> target)
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

        ActiveTop(mainTop);
    }

    public void OnClickMenuButton(UIMenuButton menuButton)
    {
        if (menuButton.targetPanel == currentPanel)
        {
            return;
        }

        if (panelStack.Count > 0)
        {
            while (panelStack.Count > 0)
            {
                panelStack.Pop().Close();
            }

            currentPanel = null;
            ResetTop();
            ActiveTop(mainTop);
        }

        OpenPanel(menuButton.targetPanel);
    }

    public void OpenPanel(BasePanel panel)
    {
        if (currentPanel != null)
        {
            if (panel == null || !panel.IsTranslucent)
            {
                ClosePanel();
            }
        }

        currentPanel = panel;

        if (currentPanel != null)
        {
            ResetTop();
            currentPanel.Open();
            currentPanel.gameObject.SetActive(true);
            mainScreen.SetActive(currentPanel.IsTranslucent);
        }
        else
        {
            mainScreen.SetActive(true);
        }
    }

    public void ClosePanel()
    {
        currentPanel.Close();
        currentPanel.gameObject.SetActive(false);
        currentPanel = null;

        ResetTop();
        ActiveTop(mainTop);
    }

    public void AddPanel(BasePanel panel)
    {
        if (currentPanel != null)
        {
            panelStack.Push(currentPanel);
        }

        OpenPanel(panel);
    }

    public void BackPanel()
    {
        bool isTranslucent = currentPanel.IsTranslucent;

        if (panelStack.Count > 0)
        {
            if (isTranslucent)
            {
                ClosePanel();
                currentPanel = panelStack.Pop();
                ResetTop();
                ActiveTop(currentPanel.ActiveTop);
            }
            else
            {
                OpenPanel(panelStack.Pop());
            }
        }
        else
        {
            ClosePanel();
            mainScreen.SetActive(true);
        }
    }

    public T Get<T>() where T : BasePanel
    {
        return panels.Find(x => x.GetType() == typeof(T)) as T;
    }

    void UpdatePanel()
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentPanel != null)
            {
                BackPanel();
            }
            else
            {
               CommonPanel commonPanel = Get<CommonPanel>();
               commonPanel.SetYesNo("Exit", Application.Quit);
               AddPanel(commonPanel);
            }
        }
    }

    public void PanelClose(Type target)
    {
        if (currentPanel != null && currentPanel.GetType() == target)
        {
            BackPanel();
        }
    }
}
