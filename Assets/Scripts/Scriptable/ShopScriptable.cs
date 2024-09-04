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
        public string nameKey;
        public List<Data> datas = new List<Data> ();
    }

    [System.Serializable]
    public class Data
    {
        [SerializeField] string nameKey;
        [SerializeField] int needValue;

        public string NameKey => nameKey;
        public int NeedValue => needValue;
    }

    public List<Data> GetData(ClientEnum.Shop shop) => datas.Find(x => x.key == shop).value.datas;
}
