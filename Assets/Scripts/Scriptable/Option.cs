using System.Collections.Generic;
[System.Serializable]
public class Option
{
    public int target;
    public List<GradeValue> gradeValue;
    [System.Serializable]
    public class GradeValue
    {
        public int key;
        public float value;
    }

    public string local;
}
