using JsonClass;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField, ReadOnly] ClientEnum.GameMode mode;
    [SerializeField, ReadOnly] StageResultPanel resultPanel;
    [SerializeField, ReadOnly] Map map;

    public ClientEnum.GameMode Mode => mode;

    StageData data;

    public void Set(ClientEnum.GameMode gameMode)
    {
        mode = gameMode;
        data = ScriptableManager.Instance.Get<StageDataScriptable>(ScriptableType.StageData).Get(DataManager.Instance.GetInfo.Stage);
        UIManager.Instance.SetStageTitle(data.nameKey, data.index);
        CreateMap(data.map);

        switch (gameMode)
        {
            case ClientEnum.GameMode.Stage:
                map.CreateEnemy(data.monsters);
                break;
            case ClientEnum.GameMode.Boss:
                map.CreateEnemy(data.monsters);
                map.CreateBoss(data.boss);
                break;
            default:
                break;
        }
    }

    public void EndStage()
    {
        switch (mode)
        {
            case ClientEnum.GameMode.Stage:
                map.CreateEnemy(data.monsters);
                break;
            case ClientEnum.GameMode.Boss:
                StageSuccess();
                break;
            default:
                break;
        }
    }

    public void CheckStage()
    {
        switch (mode)
        {
            case ClientEnum.GameMode.Stage:
                break;
            case ClientEnum.GameMode.Boss:
                break;
            default:
                break;
        }
    }

    public void StageFail()
    {
        resultPanel.SetResult(false);
        UIManager.Instance.AddPanel(resultPanel);
    }

    public void StageSuccess()
    {
        for (int i = 0; i < data.stageRewards.Count; i++)
        {
            StageRewards rewards = data.stageRewards[i];
            DataManager.Instance.AddGoods(rewards.Goods(),rewards.value);
            resultPanel.AddGoods(rewards.Goods(),rewards.value);
        }

        resultPanel.SetResult(true, mode);
        UIManager.Instance.AddPanel(resultPanel);
    }

    void CreateMap(string mapData)
    {
        if (map != null && map.name == mapData)
        {
            return;
        }
        else
        {
            if (map != null)
            {
                PoolManager.Instance.Enqueue(mapData, map.gameObject);
            }

            map = PoolManager.Instance.Dequeue(ClientEnum.ObjectType.Map, mapData).GetComponent<Map>();
            map.transform.position = Vector3.zero;
            map.gameObject.SetActive(true);
        }
    }
}
