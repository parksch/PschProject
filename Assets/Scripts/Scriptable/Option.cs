using System.Collections.Generic;
//using symbol
namespace JsonClass
{
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
