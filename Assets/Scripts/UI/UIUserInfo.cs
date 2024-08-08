using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUserInfo : MonoBehaviour
{
    [SerializeField, ReadOnly] Slider hp;
    [SerializeField, ReadOnly] Slider exp;
    [SerializeField, ReadOnly] Text playerName;
    [SerializeField, ReadOnly] Text playerLevel;

    public void SetHP(float value) => hp.value = value;
    public void SetExp(float value) => exp.value = value;

    public void Init()
    {
        playerName.text = DataManager.Instance.GetInfo.userName;
        playerLevel.text = DataManager.Instance.GetInfo.currentLevel.ToString();
        SetHP(GameManager.Instance.Player.GetHPRatio);
        SetExp(DataManager.Instance.ExpRatio());
    }
}
