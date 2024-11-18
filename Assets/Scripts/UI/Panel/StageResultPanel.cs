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
    [SerializeField] Text autoReturn;
    [SerializeField] GameObject scroll;
    [SerializeField] string challengeSuccess;
    [SerializeField] string challengeFailed;

    bool isWin;
    bool isOn = false;
    float timer = 5f;
    float current = 0;
    int count = 0;

    public void SetResult(bool _isWin)
    {
        isWin = _isWin;

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
        if (rewards.Count < count)
        {
            UIRewardSlot prefab = Instantiate(rewardSlotPrefab, content);
            rewards.Add(prefab);
        }

        rewards[count].SetGoods(goods, value);
        count++;
    }

    public void OnClickBack()
    {
        UIManager.Instance.BackPanel();
        isOn = false;
    }

    public override void Open()
    {
        content.anchoredPosition = Vector2.zero;
        current = 0;
        isOn = true;
        base.Open();
    }

    public override void Close()
    {
        for (int i = 0; i < rewards.Count; i++)
        {
            rewards[i].gameObject.SetActive(false);
        }

        count = 0;
    }

    void FixedUpdate()
    {
        if (!isOn)
        {
            return;
        }

        if (current <= timer)
        {
            current += Time.deltaTime;
        }
        else
        {
            OnClickBack();
        }
    }
}
