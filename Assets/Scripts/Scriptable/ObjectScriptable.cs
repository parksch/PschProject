using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClientEnum;

[CreateAssetMenu(fileName = "Object", menuName = "Scriptable/Object")]
public class ObjectScriptable : BaseScriptable
{
    [SerializeField] List<ObjectPrefab> objectPrefabList;

    [System.Serializable]
    public class ObjectPrefab
    {
        public string name;
        public GameObject gameObject;
        public long attack;
        public long hp;
        public long defense;
        public float attackRange;
        public float attackSpeed;
        public float moveSpeed;
        public ObjectType objectType;
    }


    public ObjectPrefab GetObject(string name) => objectPrefabList.Find(x => x.name == name);
}
