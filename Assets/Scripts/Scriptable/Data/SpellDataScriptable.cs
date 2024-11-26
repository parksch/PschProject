using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class SpellDataScriptable : ScriptableObject
    {
        public List<SpellData> spellData = new List<SpellData>();
    }

    [System.Serializable]
    public partial class SpellData
    {
        public string id;
        public string animation;
        public int state;
        public float addValue;
        public List<BuffData> buffs;
    }

    [System.Serializable]
    public partial class BuffData
    {
        public int targetState;
        public float timer;
        public float value;
        public string prefab;
    }
}
