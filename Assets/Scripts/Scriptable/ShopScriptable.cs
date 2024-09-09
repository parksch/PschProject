using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Shop", menuName = "Scriptable/Shop")]
public class ShopScriptable : BaseScriptable
{
    [SerializeField] List<Datas.Pair<ClientEnum.Shop,Category>> datas = new List<Datas.Pair<ClientEnum.Shop,Category>> ();

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
        [SerializeField] string nameKey;
        [SerializeField] int needValue;
        [SerializeField] ClientEnum.Goods goods;

        public ClientEnum.Goods GoodsType;
        public string NameKey => nameKey;
        public int NeedValue => needValue;
    }

    public Category GetData(ClientEnum.Shop shop)
    {
        Datas.Pair<ClientEnum.Shop, Category> pair = datas.Find(x => x.key == shop);

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
