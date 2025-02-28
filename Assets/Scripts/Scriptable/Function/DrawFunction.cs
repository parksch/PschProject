using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class DrawScriptable // This Class is a functional Class.
    {
        public int RequiredExp => (int)ScriptableManager.Instance.Get<DefaultValuesScriptable>(ScriptableType.DefaultValues).Get("RequiredExp");

        public Shops Target() { return draw.Find(x => (ClientEnum.Draw)x.index == ClientEnum.Draw.Scrap).type.shops[0]; }

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

        public ClientEnum.DrawValue DrawValue()
        {
            return (ClientEnum.DrawValue)type;
        }

        public ClientEnum.Goods Goods()
        {
            return (ClientEnum.Goods)goods;
        }

        public ClientEnum.Item Target()
        {
            return (ClientEnum.Item)target;
        }

        int GetAddProbability(Probabilities probability,int lv)
        {
            int value = (lv >= probability.targetLevel ? Mathf.RoundToInt(probability.value * lv * probability.addValue) : 0);

            return value;
        }

        int MaxProbability(int lv)
        {
            int value = 0;

            foreach (var item in probabilities)
            {
                value += item.value + GetAddProbability(item,lv);
            }

            return value;
        }

        public ClientEnum.Grade Grade(int lv = 0)
        {
            int random = Random.Range(0, MaxProbability(lv));
            ClientEnum.Grade grade = ClientEnum.Grade.Common;

            for (int i = 0; i < probabilities.Count; i++)
            {
                if (random < probabilities[i].value)
                {
                    grade = (ClientEnum.Grade)probabilities[i].grade;

                    break;
                }
                else
                {
                    random -= (probabilities[i].value + GetAddProbability(probabilities[i],lv));
                }
            }

            return grade;
        }
    }

    public partial class Probabilities
    {

    }

}
