using JsonClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] Transform boss;
    [SerializeField] List<Transform> spots;
    [SerializeField] float radius;

    public void CreateEnemy(List<string> monsters, StageOptionScriptable stageScriptable)
    {
        for (int i = 0; i < stageScriptable.maxEnemyCount; i++)
        {
            MonsterData monsterData = ScriptableManager.Instance.Get<MonsterDataScriptable>(ScriptableType.MonsterData).Get(monsters[Random.Range(0, monsters.Count)]);
            Vector3 center = spots[Random.Range(0, spots.Count)].position;
            GameObject gameObject = PoolManager.Instance.Dequeue(ClientEnum.ObjectType.Enemy, monsterData.prefab);
            gameObject.transform.position = new Vector3((center.x + Random.Range(-radius, radius)),0, (center.z + Random.Range(-radius, radius)));
            EnemyCharacter character = gameObject.GetComponent<EnemyCharacter>();

            character.SetState(monsterData);
            character.Init();
            gameObject.SetActive(true);
            character.SetInitializeState();
            GameManager.Instance.AddEnemy(character);
        }
    }

    public void CreateBoss(string monster, StageOptionScriptable stageScriptable)
    {
        MonsterData monsterData = ScriptableManager.Instance.Get<MonsterDataScriptable>(ScriptableType.MonsterData).Get(monster);
        GameObject gameObject = PoolManager.Instance.Dequeue(ClientEnum.ObjectType.Enemy, monsterData.prefab);
        EnemyCharacter character = gameObject.GetComponent<EnemyCharacter>();

        character.SetState(monsterData,true);
        character.Init();
        gameObject.SetActive(true);
        character.SetInitializeState();
        GameManager.Instance.AddEnemy(character);
        gameObject.transform.position = boss.position;
    }

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < spots.Count; i++)
        {
            Gizmos.DrawWireSphere(spots[i].position, radius);
        }
    }
}
