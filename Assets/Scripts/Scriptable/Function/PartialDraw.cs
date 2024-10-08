using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class DrawScriptable
    {
        [SerializeField] int requiredExp;
        [SerializeField] List<Datas.Pair<ClientEnum.Draw, Category>> datas = new List<Datas.Pair<ClientEnum.Draw, Category>>();

        public int RequiredExp => requiredExp;

        [System.Serializable]
        public class Category
        {
            [SerializeField] string nameKey;
            [SerializeField] List<Data> datas = new List<Data>();

            public string NameStringKey => nameKey;
            public List<Data> Datas => datas;
        }

        [System.Serializable]
        public class Probability
        {
            [SerializeField] ClientEnum.Grade grade;
            [SerializeField] int value;

            public ClientEnum.Grade Grade => grade;
            public int Value => value;
        }

        [System.Serializable]
        public class Data
        {
            [SerializeField] int limit;
            [SerializeField] int maxLevel;
            [SerializeField] string nameKey;
            [SerializeField] string descKey;
            [SerializeField] int needValue;
            [SerializeField] ClientEnum.Goods goods;
            [SerializeField] ClientEnum.Item target;
            [SerializeField] List<Probability> probabilities;

            public int MaxProbability
            {
                get
                {
                    int value = 0;

                    foreach (var item in probabilities)
                    {
                        value += item.Value;
                    }

                    return value;
                }
            }

            public List<Probability> Probabilities => probabilities;
            public ClientEnum.Item Target => target;
            public ClientEnum.Goods Goods => goods;
            public string DescKey => descKey;
            public string NameKey => nameKey;
            public int NeedValue => needValue;
            public int Limit => limit;
            public int MaxLevel => maxLevel;

            public ClientEnum.Grade Grade
            {
                get
                {
                    int random = Random.Range(0, MaxProbability);
                    ClientEnum.Grade grade = ClientEnum.Grade.Normal;

                    for (int i = 0; i < probabilities.Count; i++)
                    {
                        if (random < probabilities[i].Value)
                        {
                            grade = probabilities[i].Grade;

                            break;
                        }
                        else
                        {
                            random -= probabilities[i].Value;
                        }
                    }

                    return grade;
                }
            }
        }

        public Category GetData(ClientEnum.Draw shop)
        {
            Datas.Pair<ClientEnum.Draw, Category> pair = datas.Find(x => x.key == shop);

            if (pair == null)
            {
                return null;
            }
            else
            {
                return pair.value;
            }
        }

        public List<Datas.Pair<ClientEnum.Draw, Category>> Datas => datas;
    }

    public partial class Draw
    {
    }

    public partial class Type
    {
    }

    public partial class Shops
    {
    }

    public partial class Probabilities
    {
    }

}
