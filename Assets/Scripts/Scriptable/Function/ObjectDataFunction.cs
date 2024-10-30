using ClientEnum;
using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class ObjectDataScriptable // This Class is a functional Class.
    {
        public List<ObjectPrefab> objectPrefabList;

        [System.Serializable]
        public class ObjectPrefab
        {
            public string name;
            public string prefab;
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

    public partial class ObjectData
    {
    }

}
