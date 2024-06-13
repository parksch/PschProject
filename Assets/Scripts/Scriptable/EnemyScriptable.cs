using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable/Enemy")]
public class EnemyScriptable : BaseScriptable
{
    [SerializeField] List<EnemyPrefab> enemyPrefabList;

    [System.Serializable]
    public class EnemyPrefab
    {
        public string name;
        public GameObject GameObject;
    }
}
