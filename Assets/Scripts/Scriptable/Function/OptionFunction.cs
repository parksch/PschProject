using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class OptionScriptable // This Class is a functional Class.
    {
        [System.Serializable]
        public class ResultOption
        {
            [SerializeField] List<Option> datas = new List<Option>();
        }

        public List<Datas.Pair<ClientEnum.State,(ClientEnum.Grade grade,float value)>> GetRandomOption(List<ClientEnum.State> randStates, ClientEnum.Grade target)
        {
            List<Datas.Pair<ClientEnum.State, (ClientEnum.Grade grade, float value)>> option = new List<Datas.Pair<ClientEnum.State, (ClientEnum.Grade grade, float value)>>();
            OptionProbability probability = ScriptableManager.Instance.Get<OptionProbabilityScriptable>(ScriptableType.OptionProbability).GetOptionProbability(target);

            int rand = Random.Range(0, probability.MaxProbabilityCount());
            int resultCount = 0;

            for (int i = 0; i < probability.randomCount.Count; i++)
            {
                if (rand < probability.randomCount[i].value)
                {
                    resultCount = probability.randomCount[i].key;
                    break;
                }
                else
                {
                    rand -= probability.randomCount[i].value;
                }
            }

            for (int i = 0; i < resultCount; i++)
            {
                rand = Random.Range(0, probability.MaxProbabilityGrade());
                for (int j = 0; j < probability.randomGrade.Count; j++)
                {
                    if (rand < probability.randomGrade[i].value)
                    {
                        ClientEnum.Grade grade = probability.randomGrade[i].Grade();
                        ClientEnum.State state = randStates[Random.Range(0, randStates.Count)];

                        Datas.Pair<ClientEnum.State,(ClientEnum.Grade grade, float)> pair = new Datas.Pair<ClientEnum.State, (ClientEnum.Grade grade, float)>(state,(grade, GetData(state).Value(grade)));
                        option.Add(pair);
                        break;
                    }
                    else
                    {
                        rand -= probability.randomGrade[i].value;
                    }
                }
            }

            return option;
        }

        public Option GetData(ClientEnum.State target) => option.Find(x => x.Target() == target);

        public ResultOption GetGradeOption(ClientEnum.Grade grade)
        {
            ResultOption resultOption = new ResultOption();

            return resultOption;
        }
    }

    public partial class Option
    {
        public ClientEnum.State Target()
        {
            return (ClientEnum.State)target;
        }

        public float Value(ClientEnum.Grade grade)
        {
            float result = 0;
            float min = ScriptableManager.Instance.Get<DefaultValuesScriptable>(ScriptableType.DefaultValues).Get("OptionMin");
            float max = ScriptableManager.Instance.Get<DefaultValuesScriptable>(ScriptableType.DefaultValues).Get("OptionMax");

            result = Mathf.Round(gradeValue.Find(x => (ClientEnum.Grade)x.key == grade).value * Random.Range(min, max) * 100f) / 100f;
            return result;
        }
    }

    public partial class GradeValue
    {
    }

}
