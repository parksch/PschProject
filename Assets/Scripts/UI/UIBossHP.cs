using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBossHP : MonoBehaviour
{
    [SerializeField, ReadOnly] Slider hpSlider;
    [SerializeField, ReadOnly] Text hpText;
    [SerializeField, ReadOnly] Text nameText;
    [SerializeField, ReadOnly] Text timeText;
    [SerializeField] float targetTime;

    public void SetHP(UNBigStats hp) 
    {
        targetHp = hp;
        currentHp = targetHp;
        hpSlider.value = 1;
        hpText.text = $"{currentHp.Text}/{targetHp.Text}";
    }
    public void SetTime(float time) => targetTime = time;
    public void SetName(string name) => nameText.text = name;
    public void UpdateHp(UNBigStats hp)
    {
        currentHp = hp;
        hpSlider.value = currentHp/targetHp;
        hpText.text = $"{currentHp.Text}/{targetHp.Text}";
    }
    public void SetOn() => isOn = true;

    bool isOn;
    UNBigStats targetHp;
    UNBigStats currentHp;

    private void FixedUpdate()
    {
        if (Mathf.FloorToInt(targetTime) > 0)
        {
            if (GameManager.Instance.Enemies.Count == 0)
            {
                return;
            }

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
                GameManager.Instance.StageFail();
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
