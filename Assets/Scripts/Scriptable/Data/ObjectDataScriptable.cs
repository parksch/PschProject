using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class ObjectDataScriptable : ScriptableObject
    {
        public List<ObjectData> objectData = new List<ObjectData>();
    }

    [System.Serializable]
    public partial class ObjectData
    {
        public string name;
        public string prefab;
        public int attack;
        public int hp;
        public int defense;
        public float attackRange;
        public float attackSpeed;
        public float moveSpeed;
        public int objectType;
    }

}
