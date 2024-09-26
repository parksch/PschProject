using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable/Item")]
public class ItemScriptable : BaseScriptable
{
    [SerializeField] List<TypeData> itemTypeDatas = new List<TypeData>();
    

    [System.Serializable]
    public class Info
    {
        [SerializeField] string id;
        [SerializeField] GameObject prefab;
        [SerializeField] List<Datas.Pair<ClientEnum.State, float>> states = new List<Datas.Pair<ClientEnum.State, float>>();

        public string ID => id;
        public GameObject Prefab => prefab;
        public List<Datas.Pair<ClientEnum.State, float>> States => states;
    }

    public class None : Info
    {

    }

    [System.Serializable]
    public class TypeData
    {
        [SerializeField] List<ClientEnum.State> randomTarget;
        [SerializeField] ClientEnum.Item target;
        [SerializeField] List<Info> items;

        public ClientEnum.Item Target => target;
        public List<Info> Items => items;
        public List<ClientEnum.State> RandomOptionTarget => randomTarget;
    }

    public Info GetItem(DataManager.InventoryData inventoryData)
    {
        if (inventoryData.itemType == ClientEnum.Item.None)
        {
            return new None();
        }

        return (itemTypeDatas.Find(x => x.Target == inventoryData.itemType)).Items.Find(x => x.ID == inventoryData.id);
    }
}
