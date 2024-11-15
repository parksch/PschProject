using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing.MiniJSON;
using UnityEngine.UI;

public class UIBossHP : MonoBehaviour
{
    [SerializeField, ReadOnly] Slider hpSlider;
    [SerializeField, ReadOnly] Text hpText;
    [SerializeField, ReadOnly] Text nameText;
    [SerializeField, ReadOnly] Text timeText;
    bool isOn;

    public void SetHP(long hp) 
    {
        targetHp = hp;
        currentHp = targetHp;
        hpSlider.value = 1;
        hpText.text = $"{currentHp}/{targetHp}";
    }
    public void SetTime(float time) => targetTime = time;
    public void SetName(string name) => nameText.text = name;
    public void UpdateHp(long hp)
    {
        currentHp = hp;
        hpSlider.value = (float)currentHp/targetHp;
        hpText.text = $"{currentHp}/{targetHp}";
    }
    public void SetOn() => isOn = true;

    float targetTime;

    long targetHp;
    long currentHp;


    private void FixedUpdate()
    {
        if (targetTime > 0)
        {
            targetTime -= Time.deltaTime;
            timeText.text = FormatTime(targetTime);
        }
        else
        {
            if (isOn)
            {
                isOn = false;
                targetTime = 0;
                timeText.text = "00:00:00";
            }
        }

    }

    string FormatTime(float current)
    {
        int total = Mathf.FloorToInt(current);
        int hours = total / 3600;
        int minutes = (total % 3600) / 60;
        int seconds = total % 60;

        return $"{hours:D2}:{minutes:D2}:{seconds:D2}";
    }
}
