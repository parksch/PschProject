using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
}
