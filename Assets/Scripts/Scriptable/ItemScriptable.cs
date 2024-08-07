using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable/Item")]
public class ItemScriptable : BaseScriptable
{
    [SerializeField] List<ItemTypeData> itemTypeDatas = new List<ItemTypeData>();

    [System.Serializable]
    public class ItemData
    {
        public string name;
    }

    public class NoneItem : ItemData
    {

    }

    [System.Serializable]
    public class Weapon : ItemData
    {

    }

    [System.Serializable]
    public class Armor : ItemData
    {

    }

    [System.Serializable]
    public class Boots : ItemData
    {

    }

    [System.Serializable]
    public class Earring : ItemData
    {

    }

    [System.Serializable]
    public class Chain : ItemData
    {

    }

    [System.Serializable]
    public class Helmet : ItemData
    {

    }

    [System.Serializable]
    public class ItemTypeData
    {
        public ClientEnum.Item target;
        public List<ItemData> items;
    }

    public T GetItem<T>(DataManager.InventoryData inventoryData) where T : ItemData
    {
        if (inventoryData.itemType == ClientEnum.Item.None)
        {
            return new NoneItem() as T;
        }

        return (itemTypeDatas.Find(x => x.target == inventoryData.itemType)).items.Find(x => x.name == inventoryData.id) as T;
    }
}
