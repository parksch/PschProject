using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class DefaultValuesScriptable : ScriptableObject
    {
        public List<DefaultValues> defaultValues = new List<DefaultValues>();
    }

    [System.Serializable]
    public partial class DefaultValues
    {
        public string key;
        public float value;
    }

}
