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
    BuffData target;
    float value;

    public string ID => target == null ? "" : target.name;

    public void SetValue(float target,float timer)
    {
        front.fillAmount = 0;
        value = target;
        max = timer;
        current = max;
    }

    public void SetSlot(BuffData buffData,float _value,float timer)
    {
        target = buffData;
        image.sprite = buffData.Sprite();
        front.fillAmount = 0;
        value = _value;
        max = timer;
        current = max;
        text.text = current + "s";
        gameObject.SetActive(true);
    }

    public void End()
    {
        current = 0;
        text.text = current + "s";
        front.fillAmount = 1;
        gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        if (current > 0)
        {
            current -= Time.deltaTime;
            current *= 100;
            current = Mathf.Round(current)/100f;
            text.text = current.ToString("F2") + "s";
            front.fillAmount = (max - current)/max;
        }
        else
        {
            End();
        }
    }
}
