using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BasePanel : MonoBehaviour
{
    [SerializeField] bool isTranslucent;
    [SerializeField] protected List<GameObject> activeTopUI = new List<GameObject>();

    public List<GameObject> ActiveTop => activeTopUI;
    public bool IsTranslucent => isTranslucent;
    public void CopyTopMenu(List<GameObject> list)
    {
        activeTopUI.Clear();
        activeTopUI = list.ToList();
    }
    public virtual void OnUpdate() { }
    public virtual void FirstLoad() { }

    public virtual void Open() 
    {
        UIManager.Instance.ActiveTop(activeTopUI);
    }

    public virtual void Close() { }
}
