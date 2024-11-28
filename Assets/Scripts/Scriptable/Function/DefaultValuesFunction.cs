using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class DefaultValuesScriptable// This Class is a functional Class.
    {
        Dictionary<string, float> keyValuePairs = null; 

        public float Get(string key) 
        {
            if (keyValuePairs == null)
            {
                keyValuePairs = new Dictionary<string, float>();

                foreach (var item in defaultValues)
                {
                    keyValuePairs[item.key] = item.value;
                }
            }

            return defaultValues.Find(x => x.key == key).value; 
        }
    }

    public partial class DefaultValues
    {
    }

}
