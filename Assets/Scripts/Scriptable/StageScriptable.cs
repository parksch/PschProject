using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage",menuName = "Scriptable/Stage")]
public class StageScriptable : BaseScriptable
{
    [SerializeField] int maxMonsterCount;
    [SerializeField] float multiplyPerStageHp;
    [SerializeField] float multiplyPerStageDefanse;
    [SerializeField] float multiplyPerStageAttack;

}
