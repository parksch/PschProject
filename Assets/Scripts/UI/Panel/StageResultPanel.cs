using JsonClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageResultPanel : BasePanel
{
    [SerializeField,ReadOnly] Text title;
    [SerializeField] RectTransform content;
    [SerializeField] UIRewardSlot rewardSlotPrefab;
    [SerializeField] List<UIRewardSlot> rewards;
    [SerializeField] Text failDesc;
    [SerializeField] GameObject scroll;
    [SerializeField] string challengeSuccess;
    [SerializeField] string challengeFailed;

    public void SetResult(bool isWin)
    {
        if (isWin)
        {
            title.text = ScriptableManager.Instance.Get<LocalizationScriptable>(ScriptableType.Localization).Get(challengeSuccess);
        }
        else
        {
            title.text = ScriptableManager.Instance.Get<LocalizationScriptable>(ScriptableType.Localization).Get(challengeFailed);
        }

        failDesc.gameObject.SetActive(!isWin);
        scroll.SetActive(isWin);
    }

    public void AddGoods(ClientEnum.Goods goods,int value)
    {

    }

    public override void Open()
    {
        content.anchoredPosition = Vector2.zero;
        base.Open();
    }

    public override void Close()
    {
        for (int i = 0; i < rewards.Count; i++)
        {
            rewards[i].gameObject.SetActive(false);
        }
    }
}
