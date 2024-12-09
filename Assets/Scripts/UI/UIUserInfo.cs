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
    public void SetExp() => exp.value = DataManager.Instance.ExpRatio();
    public void SetLevel() => playerLevel.text = DataManager.Instance.GetInfo.CurrentLevel.ToString();

    public void Init()
    {
        playerName.text = DataManager.Instance.GetInfo.UserName;
        playerLevel.text = DataManager.Instance.GetInfo.CurrentLevel.ToString();
        SetHP(GameManager.Instance.Player.GetHPRatio);
        SetExp();

        DataManager.Instance.OnChangeExp += _ => { SetLevel(); };
        DataManager.Instance.OnChangeExp += _ => { SetExp();};
    }
}
