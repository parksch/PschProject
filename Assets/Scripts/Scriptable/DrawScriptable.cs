using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Draw", menuName = "Scriptable/Draw")]
public class DrawScriptable : BaseScriptable
{
    [SerializeField] int requiredExp;
    [SerializeField] List<Datas.Pair<ClientEnum.Draw,Category>> datas = new List<Datas.Pair<ClientEnum.Draw,Category>> ();

    public int RequiredExp => requiredExp;

    [System.Serializable]
    public class Category
    {
        [SerializeField] string nameKey;
        [SerializeField] List<Data> datas = new List<Data> ();
        
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

        public int MaxProbability()
        {
            int value = 0;

            foreach (var item in probabilities)
            {
                value += item.Value;
            }

            return value;
        }

        public List<Probability> Probabilities => probabilities;
        public ClientEnum.Goods Goods => goods;
        public string DescKey => descKey;
        public string NameKey => nameKey;
        public int NeedValue => needValue;
        public int Limit => limit;
        public int MaxLevel => maxLevel;
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
}
