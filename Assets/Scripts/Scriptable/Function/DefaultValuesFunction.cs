using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class DefaultValuesScriptable// This Class is a functional Class.
    {
        public float Get(string key) { return defaultValues.Find(x => x.key == key).value; }
    }

    public partial class DefaultValues
    {
    }

}
