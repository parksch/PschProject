using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class DrawScriptable // This Class is a functional Class.
    {
        public int RequiredExp => (int)ScriptableManager.Instance.Get<DefaultValuesScriptable>(ScriptableType.DefaultValues).Get("RequireExp");

        public Draw GetData(ClientEnum.Draw shop)
        {
            Draw data = draw.Find(x => (ClientEnum.Draw) x.index == shop);

            if (data == null)
            {
                return null;
            }
            else
            {
                return data;
            }
        }
    }

    public partial class Draw
    {
    }

    public partial class Type
    {
    }

    public partial class Shops
    {
        public ClientEnum.Goods Goods()
        {
            return (ClientEnum.Goods)goods;
        }

        public ClientEnum.Item Target()
        {
            return (ClientEnum.Item)target;
        }

        int MaxProbability()
        {
            int value = 0;

            foreach (var item in probabilities)
            {
                value += item.value;
            }

            return value;
        }

        public ClientEnum.Grade Grade()
        {
            int random = Random.Range(0, MaxProbability());
            ClientEnum.Grade grade = ClientEnum.Grade.Normal;

            for (int i = 0; i < probabilities.Count; i++)
            {
                if (random < probabilities[i].value)
                {
                    grade = (ClientEnum.Grade)probabilities[i].grade;

                    break;
                }
                else
                {
                    random -= probabilities[i].value;
                }
            }

            return grade;
        }
    }

    public partial class Probabilities
    {
    }

}
