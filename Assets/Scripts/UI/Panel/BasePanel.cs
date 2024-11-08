using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel : MonoBehaviour
{
    [SerializeField] protected List<GameObject> openMenu = new List<GameObject>();

    public virtual void OnUpdate() { }
    public virtual void FirstLoad() { }

    public virtual void Open() 
    {
        UIManager.Instance.OpenTop(openMenu);
    }

    public virtual void Close() { }
}
