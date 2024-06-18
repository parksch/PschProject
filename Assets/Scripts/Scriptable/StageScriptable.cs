using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage",menuName = "Scriptable/Stage")]
public class StageScriptable : BaseScriptable
{
    [SerializeField] int maxEnemyCount;
    [SerializeField] float multiplyPerStageHp;
    [SerializeField] float multiplyPerStageDefanse;
    [SerializeField] float multiplyPerStageAttack;
    [SerializeField] List<StageMonster> stageMonsters;

    public int MaxEnemyCount => maxEnemyCount;
    public float MultiplyPerStageHp => multiplyPerStageHp;
    public float MultiplyPerStageDefanse => multiplyPerStageDefanse;
    public float MultiplyPerStageAttack => multiplyPerStageAttack;

    [System.Serializable]
    public class StageMonster
    {
        public List<string> monsters;
    }

    public List<string> GetMonsters(int stage)
    {
        List<string> result = null;

        if (stage > stageMonsters.Count)
        {
            result = stageMonsters[stageMonsters.Count - 1].monsters;
        }
        else
        {
            result = stageMonsters[stage].monsters;
        }

        return result;
    }
}
