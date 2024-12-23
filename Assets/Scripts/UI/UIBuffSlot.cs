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

    BuffData target;
    float value;

    public string ID => target == null ? "" : target.name;

    public void SetValue(float target,float timer)
    {
        front.fillAmount = 0;
        value = target > value ? target : value;
        max = timer;
        current = max;
    }

    public void SetSlot(BuffData buffData,float _value,float timer)
    {
        target = buffData;
        image.sprite = ResourcesManager.Instance.GetSprite(target.atlas, target.sprite);
        front.fillAmount = 0;
        value = _value;
        max = timer;
        current = max;
        text.text = current + "s";
        gameObject.SetActive(true);
    }

    void FixedUpdate()
    {
        if (current > 0)
        {
            current -= Time.deltaTime;
            current *= 10;
            current = Mathf.Floor(current)/10f;
            text.text = current + "s";
            front.fillAmount = (max - current)/max;
        }
        else
        {
            current = 0;
            text.text = current + "s";
            front.fillAmount = 1;
            gameObject.SetActive(false);
        }
    }

}
