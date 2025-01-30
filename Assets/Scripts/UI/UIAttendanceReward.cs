using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAttendanceReward : UIRewardSlot
{
    [SerializeField] Text dayText;
    [SerializeField] GameObject front;

    public void Set(int day,ClientEnum.Goods goods,int value)
    {
        dayText.text = "Day" + day;
        SetGoods(goods,value);
        front.SetActive(DataManager.Instance.Weekly + 1 >= day);
    }
}
