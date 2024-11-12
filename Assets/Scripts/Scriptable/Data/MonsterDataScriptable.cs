using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class MonsterDataScriptable : ScriptableObject
    {
        public List<MonsterData> monsterData = new List<MonsterData>();
    }

    [System.Serializable]
    public partial class MonsterData
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
