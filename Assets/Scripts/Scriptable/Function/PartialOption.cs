using ClientEnum;
using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class OptionScriptable
    {
        [SerializeField] List<Data> datas = new List<Data>();
        [SerializeField] List<Probability> addOption = new List<Probability>();

        [System.Serializable]
        public class Probability
        {
            [SerializeField] ClientEnum.Grade target;
            [SerializeField] List<Datas.Pair<int, int>> count;
            [SerializeField] List<Datas.Pair<ClientEnum.Grade, int>> gradeValue;

            public int MaxProbabilityCount
            {
                get
                {
                    int value = 0;
                    foreach (var item in count)
                    {
                        value += item.value;
                    }

                    return value;
                }
            }

            public int MaxProbabilityGrade
            {
                get
                {
                    int value = 0;
                    foreach (var item in gradeValue)
                    {
                        value += item.value;
                    }

                    return value;
                }
            }

            public ClientEnum.Grade Grade => target;

            public List<Datas.Pair<int, int>> RandomCount => count;

            public List<Datas.Pair<ClientEnum.Grade, int>> RandomGrade => gradeValue;
        }

        [System.Serializable]
        public class Data
        {
            [SerializeField] ClientEnum.State target;
            [SerializeField] float min = 0.9f, max = 1.1f;
            [SerializeField] string local;
            [SerializeField] List<Datas.Pair<ClientEnum.Grade, float>> gradeValue = new List<Datas.Pair<ClientEnum.Grade, float>>();

            public ClientEnum.State Target => target;
            public List<Datas.Pair<ClientEnum.Grade, float>> GradeValue => gradeValue;
            public float Value(ClientEnum.Grade grade) => Mathf.Ceil(gradeValue.Find(x => x.key == grade).value * Random.Range(min, max) * 100f) / 100f;
            public string Local => local;
        }

        [System.Serializable]
        public class ResultOption
        {
            [SerializeField] List<Data> datas = new List<Data>();
        }

        public List<Datas.Pair<ClientEnum.State, float>> GetRandomOption(List<ClientEnum.State> randStates, ClientEnum.Grade target)
        {
            List<Datas.Pair<ClientEnum.State, float>> option = new List<Datas.Pair<ClientEnum.State, float>>();
            Probability probability = addOption.Find(x => x.Grade == target);
            int rand = Random.Range(0, probability.MaxProbabilityCount);
            int resultCount = 0;

            for (int i = 0; i < probability.RandomCount.Count; i++)
            {
                if (rand < probability.RandomCount[i].value)
                {
                    resultCount = probability.RandomCount[i].key;
                    break;
                }
                else
                {
                    rand -= probability.RandomCount[i].value;
                }
            }

            for (int i = 0; i < resultCount; i++)
            {
                rand = Random.Range(0, probability.MaxProbabilityGrade);
                for (int j = 0; j < probability.RandomGrade.Count; j++)
                {
                    if (rand < probability.RandomGrade[i].value)
                    {
                        ClientEnum.Grade grade = probability.RandomGrade[i].key;
                        ClientEnum.State state = randStates[Random.Range(0, randStates.Count)];

                        Datas.Pair<ClientEnum.State, float> pair = new Datas.Pair<ClientEnum.State, float>(state, GetData(state).Value(grade));
                        option.Add(pair);
                        break;
                    }
                    else
                    {
                        rand -= probability.RandomGrade[i].value;
                    }
                }
            }

            return option;
        }

        public Data GetData(ClientEnum.State target) => datas.Find(x => x.Target == target);

        public ResultOption GetGradeOption(ClientEnum.Grade grade)
        {
            ResultOption resultOption = new ResultOption();

            return resultOption;
        }

        public List<Data> Datas => datas;
    }

    public partial class Option
    {
    }

    public partial class GradeValue
    {
    }

}
