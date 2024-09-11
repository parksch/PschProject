using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Draw", menuName = "Scriptable/Draw")]
public class DrawScriptable : BaseScriptable
{
    [SerializeField] List<Datas.Pair<ClientEnum.Draw,Category>> datas = new List<Datas.Pair<ClientEnum.Draw,Category>> ();

    [System.Serializable]
    public class Category
    {
        [SerializeField] string nameKey;
        [SerializeField] List<Data> datas = new List<Data> ();
        
        public string NameStringKey => nameKey;
        public List<Data> Datas => datas;
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
