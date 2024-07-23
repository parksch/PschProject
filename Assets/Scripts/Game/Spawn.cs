using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] List<Transform> spots;
    [SerializeField] float radius;

    public void CreateEnemy()
    {
        StageScriptable stageScriptable = TableManager.Instance.StageScriptable;
        List<string> enemies = stageScriptable.GetMonsters(DataManager.Instance.GetInfo.stage);
        Vector3 center = spots[Random.Range(0,spots.Count)].position;

        for (int i = 0; i < stageScriptable.MaxEnemyCount; i++)
        {
            GameObject gameObject = PoolManager.Instance.Dequeue(enemies[Random.Range(0, enemies.Count)]);
            gameObject.transform.position = new Vector3((center.x + Random.Range(-radius, radius)),0, (center.z + Random.Range(-radius, radius)));
            EnemyCharacter character = gameObject.GetComponent<EnemyCharacter>();

            character.Init();
            gameObject.SetActive(true);
            character.SetInitializeState();
            GameManager.Instance.AddEnemy(character);
        }
    }


    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < spots.Count; i++)
        {
            Gizmos.DrawWireSphere(spots[i].position, radius);
        }
    }
}
