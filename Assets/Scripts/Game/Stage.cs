using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField,ReadOnly] StageScriptable stageScriptable;
    [SerializeField,ReadOnly] Spawn spawn;

    public void StartStage()
    {
        spawn.CreateEnemy(stageScriptable.GetMonsters(DataManager.Instance.GetInfo.stage),stageScriptable.MaxEnemyCount);
    }

    public void EndStage()
    {

    }
}
