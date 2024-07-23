using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField,ReadOnly] Spawn spawn;

    public void StartStage()
    {
        CreateEnemy();
    }

    public void EndStage()
    {

    }

    public void CreateEnemy()
    {
        spawn.CreateEnemy();
    }
}
