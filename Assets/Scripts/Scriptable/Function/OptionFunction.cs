using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class OptionScriptable // This Class is a functional Class.
    {
        [System.Serializable]
        public class ResultOption
        {
            [SerializeField] List<Option> dates = new List<Option>();
        }

        public List<Datas.Pair<ClientEnum.State,ItemOption >> GetRandomOption(List<ClientEnum.State> randStates, ClientEnum.Grade target)
        {
            List<Datas.Pair<ClientEnum.State,ItemOption >> option = new List<Datas.Pair<ClientEnum.State, ItemOption>>();
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
                    if (rand < probability.randomGrade[j].value)
                    {
                        ClientEnum.Grade grade = probability.randomGrade[j].Grade();
                        ClientEnum.State state = randStates[Random.Range(0, randStates.Count)];
                        ClientEnum.ChangeType changeType = Random.Range(0, 10) > 5 ? ClientEnum.ChangeType.Sum:ClientEnum.ChangeType.Product;

                        Option getOption = GetData(state, changeType);

                        if (getOption == null)
                        {
                            switch (changeType)
                            {
                                case ClientEnum.ChangeType.Sum:
                                    changeType = ClientEnum.ChangeType.Product;
                                    break;
                                case ClientEnum.ChangeType.Product:
                                    changeType = ClientEnum.ChangeType.Sum;
                                    break;
                                default:
                                    break;
                            }

                            getOption = GetData(state, changeType);
                        }

                        ItemOption itemOption = new ItemOption();
                        itemOption.SetValue(changeType, getOption.Value(grade), grade);

                        Datas.Pair<ClientEnum.State,ItemOption> pair = new Datas.Pair<ClientEnum.State, ItemOption>(state,itemOption);
                        option.Add(pair);

                        break;
                    }
                    else
                    {
                        rand -= probability.randomGrade[j].value;
                    }
                }
            }

            return option;
        }

        public Option GetData(ClientEnum.State target, ClientEnum.ChangeType changeType) => option.Find(x => x.Target() == target && x.ChangeType() == changeType);

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

        public ClientEnum.ChangeType ChangeType()
        {
            return (ClientEnum.ChangeType)changeType;
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
