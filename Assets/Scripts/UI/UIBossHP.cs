using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBossHP : MonoBehaviour
{
    [SerializeField, ReadOnly] Text nameText;
    [SerializeField, ReadOnly] Slider hpSlider;
    [SerializeField, ReadOnly] Text timeText;

    public void SetHP(long hp) 
    {
        targetHp = hp;
        currentHp = targetHp;
        hpSlider.value = 1;
    }

    public void SetTime(float time) => targetTime = time;
    public void SetName(string name) => nameText.text = name;

    float targetTime;

    long targetHp;
    long currentHp;


    private void FixedUpdate()
    {
        
    }
}
