using JsonClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBuffSlot : MonoBehaviour
{
    [SerializeField, ReadOnly] Image image;
    [SerializeField, ReadOnly] Image front;
    [SerializeField, ReadOnly] Text text;
    [SerializeField] float max;
    [SerializeField] float current;

    string desc;
    BuffBase target;
    float value;

    public string ID => target == null ? "" : target.name;

    public void SetValue(float target,float timer)
    {
        front.fillAmount = 0;
        value = target;
        max = timer;
        current = max;
    }

    public void SetSlot(BuffBase buffData,float _value,float timer)
    {
        target = buffData;
        BuffData buff = ScriptableManager.Instance.Get<BuffDataScriptable>(ScriptableType.BuffData).buffData.Find(x => target.ID == x.name);

        image.sprite = buff.Sprite();
        front.fillAmount = 0;
        value = _value;
        max = timer;
        current = max;
        text.text = current + "s";
        gameObject.SetActive(true);
    }

    public void End()
    {
        target = null;
        current = 0;
        text.text = current + "s";
        front.fillAmount = 1;
        gameObject.SetActive(false);
    }

    void Update()
    {
        current = target.Timer;

        if (current > 0)
        {
            text.text = current.ToString("F2") + "s";
            front.fillAmount = (max - current)/max;
        }
        else
        {
            End();
        }
    }
}
