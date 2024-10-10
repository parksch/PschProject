using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class OptionProbabilityScriptable : ScriptableObject
    {
        public List<OptionProbability> optionProbability;
    }

    [System.Serializable]
    public partial class OptionProbability
    {
        public int grade;
        public List<RandomCount> randomCount;
        public List<RandomGrade> randomGrade;
    }

    [System.Serializable]
    public partial class RandomCount
    {
        public int key;
        public int value;
    }

    [System.Serializable]
    public partial class RandomGrade
    {
        public int key;
        public int value;
    }

}
