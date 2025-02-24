using JsonClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIContentLock : MonoBehaviour
{
    [SerializeField] int id;

    ContentLock contentLock;

    public void Init()
    {
        contentLock = ScriptableManager.Instance.Get<ContentLockScriptable>(ScriptableType.ContentLock).Get(id);
    }

    public void Check()
    {

    }

    public void OnClickContentLock()
    {

    }
}
