using JsonClass;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class UIADButton : MonoBehaviour
{
    [SerializeField, ReadOnly] Text text;
    DateTime load;
    TimeSpan span;

    public void CheckTime()
    {
        DateTime current = DateTime.UtcNow;
        load = DateTime.UtcNow;

        if (PlayerPrefs.HasKey("AdTime"))
        {
            string time = PlayerPrefs.GetString("AdTime");
            load = DateTime.Parse(time);
        }

        span = load - current;
        
    }

    public void SetTimer()
    {
        DateTime current = DateTime.UtcNow;
        load = current.AddHours(1);

        string timeString = load.ToString("yyyy-MM-dd HH:mm:ss");
        PlayerPrefs.SetString("AdTime", timeString);
        span = load - current;
    }

    void FixedUpdate()
    {
        span = load - DateTime.UtcNow;

        if (span.TotalSeconds > 0)
        {
            text.text = string.Format("{0}:{1}:{2}",span.Hours,span.Minutes,span.Seconds);
        }
        else
        {
            text.text = ScriptableManager.Instance.Get<LocalizationScriptable>(ScriptableType.Localization).Get("ADIcon");
        }
    }

    public void OnClick(BasePanel target)
    {
        if (span.TotalSeconds > 0)
        {
            CommonPanel commonPanel = UIManager.Instance.Get<CommonPanel>();
            commonPanel.SetOK("ADCoolTime");
            UIManager.Instance.AddPanel(commonPanel);
        }
        else
        {
            UIManager.Instance.AddPanel(target);
        }
    }
}
