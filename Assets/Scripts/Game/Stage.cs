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

    public void SetBoss()
    {

    }

    public void SetStage()
    {
        mode = ClientEnum.GameMode.Stage;
        data = ScriptableManager.Instance.Get<StageDataScriptable>(ScriptableType.StageData).Get(DataManager.Instance.GetInfo.Stage);
        UIManager.Instance.SetStageTitle(data.nameKey,data.index);
        CreateMap(data.map);
        CreateEnemy();
    }

    public void EndStage()
    {

    }

    public void CreateEnemy()
    {
        map.CreateEnemy(data.monsters);
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
