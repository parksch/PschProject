using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel : MonoBehaviour
{
    [SerializeField] bool isTranslucent;
    [SerializeField] protected List<GameObject> openMenu = new List<GameObject>();

    public List<GameObject> OpenMenu => openMenu;
    public bool IsTranslucent => isTranslucent;
    public virtual void OnUpdate() { }
    public virtual void FirstLoad() { }

    public virtual void Open() 
    {
        UIManager.Instance.OpenTop(openMenu);
    }

    public virtual void Close() { }
}
