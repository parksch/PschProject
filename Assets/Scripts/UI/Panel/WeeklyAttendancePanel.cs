using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class WeeklyAttendancePanel : BasePanel
{
    [SerializeField] UIAttendanceReward prefab;
    [SerializeField] List<UIAttendanceReward> uiAttendanceRewardList;
    [SerializeField] List<AttendanceReward> attendanceRewardList;
    [SerializeField] RectTransform content;
    [SerializeField,ReadOnly] string weeklyCode = "WeeklyCode";

    [System.Serializable]
    class AttendanceReward
    {
        public ClientEnum.Goods index;
        public int value;
        public int targetDay;
    }

    public override void FirstLoad()
    {

        for (int i = 0; i < attendanceRewardList.Count; i++)
        {
            AttendanceReward reward = attendanceRewardList[i];

            if (i == 0)
            {
                uiAttendanceRewardList[0].Set(reward.targetDay, reward.index,reward.value);
            }
            else
            {
                UIAttendanceReward uIAttendance = Instantiate(prefab, content);
                uiAttendanceRewardList.Add(uIAttendance);
                uiAttendanceRewardList[i].Set(reward.targetDay, reward.index, reward.value);
            }
        }

        if (PlayerPrefs.HasKey(weeklyCode))
        {
            string time = PlayerPrefs.GetString(weeklyCode);
            DateTime load = DateTime.Parse(time);
            TimeSpan result = DataManager.Instance.CurrentDate - load;

            if (result.TotalDays >= 1)
            {
                UIManager.Instance.AddPanel(this);
                DateTime today = DateTime.UtcNow.Date;
                PlayerPrefs.SetString(weeklyCode, today.ToString("yyyy-MM-dd")); // ��¥�� ����
                PlayerPrefs.Save();
                DataManager.Instance.AddWeekly();

                AttendanceReward reward = attendanceRewardList[DataManager.Instance.Weekly];
                DataManager.Instance.AddGoods(reward.index, reward.value);
                RewardPanel rewardPanel = UIManager.Instance.Get<RewardPanel>();
                rewardPanel.AddGoods(reward.index, reward.value);
                UIManager.Instance.AddPanel(rewardPanel);
            }
        }
        else
        {
            UIManager.Instance.AddPanel(this);
            DateTime today = DateTime.UtcNow.Date;
            PlayerPrefs.SetString(weeklyCode, today.ToString("yyyy-MM-dd")); // ��¥�� ����
            PlayerPrefs.Save();
            DataManager.Instance.AddWeekly();

            AttendanceReward reward = attendanceRewardList[DataManager.Instance.Weekly];
            DataManager.Instance.AddGoods(reward.index, reward.value);
            RewardPanel rewardPanel = UIManager.Instance.Get<RewardPanel>();
            rewardPanel.AddGoods(reward.index, reward.value);
            UIManager.Instance.AddPanel(rewardPanel);
        }
    }

}
