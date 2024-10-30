using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class OptionScriptable : ScriptableObject
    {
        public List<Option> option = new List<Option>();
    }

    [System.Serializable]
    public partial class Option
    {
        public int target;
        public List<GradeValue> gradeValue;
        public string local;
    }

    [System.Serializable]
    public partial class GradeValue
    {
        public int key;
        public float value;
    }

}
