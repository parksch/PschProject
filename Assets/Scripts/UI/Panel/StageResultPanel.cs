using ClientEnum;
using JsonClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageResultPanel : BasePanel
{
    [SerializeField,ReadOnly] Text title;
    [SerializeField,ReadOnly] RectTransform content;
    [SerializeField,ReadOnly] UIRewardSlot rewardSlotPrefab;
    [SerializeField,ReadOnly] List<UIRewardSlot> rewards;
    [SerializeField,ReadOnly] Text autoText;
    [SerializeField] List<GameObject> failObjects;
    [SerializeField] List<GameObject> successObjects;
    [SerializeField,ReadOnly] string challengeSuccess;
    [SerializeField,ReadOnly] string challengeFailed;
    [SerializeField,ReadOnly] string autoContinueLocal;
    [SerializeField,ReadOnly] string autoReturnLocal;

    GameMode mode;
    LocalizationScriptable localization;
    bool isWin;
    bool isOn = false;
    float timer = 5f;
    float current = 0;
    int count = 0;

    public override void FirstLoad()
    {
        localization = ScriptableManager.Instance.Get<LocalizationScriptable>(ScriptableType.Localization);
    }

    public void SetResult(bool _isWin,GameMode gameMode = GameMode.Stage)
    {
        mode = gameMode;
        isWin = _isWin;

        if (isWin)
        {
            title.text = ScriptableManager.Instance.Get<LocalizationScriptable>(ScriptableType.Localization).Get(challengeSuccess);

            switch (mode)
            {
                case GameMode.Boss:
                    StageData stageData = ScriptableManager.Instance.Get<StageDataScriptable>(ScriptableType.StageData).Get(DataManager.Instance.GetInfo.ChallengingStage);
                    DataManager.Instance.GetInfo.ChallengingStage = stageData.next;
                    if (stageData.next != 0)
                    {
                        DataManager.Instance.GetInfo.Stage = stageData.next;
                    }
                    break;
                default:
                    break;
            }
        }
        else
        {
            title.text = ScriptableManager.Instance.Get<LocalizationScriptable>(ScriptableType.Localization).Get(challengeFailed);
        }

        for (int i = 0; i < failObjects.Count; i++)
        {
            failObjects[i].SetActive(!isWin);
        }

        for (int i = 0; i < successObjects.Count; i++)
        {
            successObjects[i].SetActive(isWin);
        }

    }
 
    public void AddGoods(ClientEnum.Goods goods,int value)
    {
        if (rewards.Count <= count)
        {
            UIRewardSlot prefab = Instantiate(rewardSlotPrefab, content);
            rewards.Add(prefab);
        }

        if (!rewards[count].CheckGoods(goods,value))
        {
            rewards[count].SetGoods(goods, value);
            count++;
        }
    }

    public void OnClickBack()
    {
        UIManager.Instance.BackPanel();
        GameManager.Instance.OnChangeGameMode(GameMode.Stage);
        isOn = false;
    }

    public void OnClickNext()
    {
        if (ChangeStage())
        {
            UIManager.Instance.BackPanel();
            GameManager.Instance.OnChangeGameMode(mode);
            isOn = false;
        }
        else
        {
            OnClickBack();
        }
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


        if (current < timer)
        {
            current += Time.deltaTime;

            if (current > timer)
            {
                current = timer;
            }

            if (isWin)
            {
                autoText.text = string.Format(localization.Get(autoContinueLocal), (int)(timer - current));
            }
            else
            {
                autoText.text = string.Format(localization.Get(autoReturnLocal), (int)(timer - current));
            }
        }
        else
        {
            if (isWin)
            {
                OnClickNext();
            }
            else
            {
                OnClickBack();
            }
        }
    }

    bool ChangeStage()
    {
        bool result = false;

        switch (mode)
        {
            case GameMode.Stage:
                break;
            case GameMode.Boss:
                if (DataManager.Instance.GetInfo.ChallengingStage != 0)
                {
                    result = true;
                }
                break;
            default:
                break;
        }

        return result;
    }
}
