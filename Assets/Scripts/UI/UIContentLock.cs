using JsonClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIContentLock : MonoBehaviour
{
    [SerializeField] int id;

    string key = "Guide";
    bool isDone = false;
    ContentLock contentLock;

    public void Init()
    {
        isDone = false;
        contentLock = ScriptableManager.Instance.Get<ContentLockScriptable>(ScriptableType.ContentLock).Get(id);
        Check();

        if (isDone)
        {
            gameObject.SetActive(false);
        }
    }

    void Check()
    {
        switch (contentLock.ContentLockType())
        {
            case ClientEnum.ContentLockType.Level:
                if (contentLock.targetValue <= DataManager.Instance.CurrentLevel)
                {
                    isDone = true;
                }
                break;
            case ClientEnum.ContentLockType.Guide:

                if (PlayerPrefs.HasKey(key))
                {
                    int code = PlayerPrefs.GetInt(key);
                    if (code == -1)
                    {
                        isDone = true;
                    }
                    else if (contentLock.targetValue <= code)
                    {
                        isDone = true;
                    }
                }
                break;
            default:
                break;
        }
    }

    public void LockCheck()
    {
        if (isDone)
        {
            return;
        }

        Check();

        if (isDone)
        {
            gameObject.SetActive(false);
            CommonPanel common = UIManager.Instance.Get<CommonPanel>();
            common.SetOKTypeText(contentLock.OpenLocal());
            UIManager.Instance.AddPanel(common);
        }
    }

    public void OnClickContentLock()
    {
        CommonPanel common = UIManager.Instance.Get<CommonPanel>();
        common.SetOKTypeText(contentLock.ClickLocal());
        UIManager.Instance.AddPanel(common);
    }
}
