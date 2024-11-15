using JsonClass;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] ClientEnum.GameMode mode;
    [SerializeField,ReadOnly] Map map;

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
                map.CreateBoss(data.boss);
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
                map.CreateEnemy(data.monsters);
                break;
            case ClientEnum.GameMode.Boss:
                break;
            default:
                break;
        }
    }

    public void StageFail()
    {

    }

    public void StageSuccess()
    {

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
