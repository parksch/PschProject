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
        public string id;
        public GameObject prefab;
        public List<Datas.Pair<ClientEnum.State, float>> states = new List<Datas.Pair<ClientEnum.State, float>>();
    }

    public class None : Info
    {

    }

    [System.Serializable]
    public class TypeData
    {
        public ClientEnum.Item target;
        public List<Info> items;
    }

    public Info GetItem(DataManager.InventoryData inventoryData)
    {
        if (inventoryData.itemType == ClientEnum.Item.None)
        {
            return new None();
        }

        return (itemTypeDatas.Find(x => x.target == inventoryData.itemType)).items.Find(x => x.id == inventoryData.id);
    }
}
