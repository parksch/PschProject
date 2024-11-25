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
        public int id;
        public float addValue;
    }

}
