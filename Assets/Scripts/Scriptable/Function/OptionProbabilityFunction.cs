using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class OptionProbabilityScriptable // This Class is a functional Class.
    {
        public OptionProbability GetOptionProbability(ClientEnum.Grade target)
        {
            return optionProbability.Find(x => (ClientEnum.Grade)x.grade == target);
        }
    }

    public partial class OptionProbability
    {
        public int MaxProbabilityCount()
        {
            int value = 0;
            foreach (var item in randomCount)
            {
                value += item.value;
            }

            return value;
        }

        public int MaxProbabilityGrade()
        {
            int value = 0;
            foreach (var item in randomGrade)
            {
                value += item.value;
            }

            return value;
        }
    }

    public partial class RandomCount
    {
    }

    public partial class RandomGrade
    {
        public ClientEnum.Grade Grade()
        {
           return (ClientEnum.Grade)key;
        }
    }

}
