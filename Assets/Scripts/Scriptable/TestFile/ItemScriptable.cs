using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable/Item")]
public class ItemScriptable : BaseScriptable
{
    [SerializeField] List<TypeData> itemTypeDatas = new List<TypeData>();
    
    public ClientEnum.Item GetRandomTarget => itemTypeDatas[Random.Range(0, itemTypeDatas.Count)].Target;

    [System.Serializable]
    public class Info
    {
        [SerializeField] string id;
        [SerializeField] Sprite sprite;
        [SerializeField] GameObject prefab;
        [SerializeField] ClientEnum.State mainState;

        public string ID => id;
        public GameObject Prefab => prefab;
        public ClientEnum.State MainState => mainState;
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
        [SerializeField] float mainStateAddValue;

        public ClientEnum.Item Target => target;
        public List<Info> Items => items;
        public List<ClientEnum.State> RandomOptionTarget => randomTarget;
    }

    //Symbol

    public TypeData GetTypeData(ClientEnum.Item target)
    {
        return itemTypeDatas.Find(x => x.Target == target);
    }

    public Info GetItem(BaseItem item)
    {
        if (item.Type == ClientEnum.Item.None)
        {
            return new None();
        }
        
        return (itemTypeDatas.Find(x => x.Target == item.Type)).Items.Find(x => x.ID == item.ID);
    }

    public Info GetRandom(ClientEnum.Item target)
    {
        TypeData typeData = GetTypeData(target);

        return typeData.Items[Random.Range(0, typeData.Items.Count)];
    }

    public List<ClientEnum.State> GetRandomOption(ClientEnum.Item target)
    {
        TypeData typeData = GetTypeData(target);

        return typeData.RandomOptionTarget;
    }

}
