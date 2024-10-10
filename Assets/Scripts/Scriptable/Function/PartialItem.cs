using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class ItemScriptable
    {
        public List<TypeData> itemTypeDatas = new List<TypeData>();

        public ClientEnum.Item GetRandomTarget => itemTypeDatas[Random.Range(0, itemTypeDatas.Count)].target;

        [System.Serializable]
        public class Info
        {
            public string id;
            public string sprite;
            public string prefab;
            public ClientEnum.State mainState;
        }

        public class None : Info
        {

        }

        [System.Serializable]
        public class TypeData
        {
            public List<ClientEnum.State> randomTarget;
            public ClientEnum.Item target;
            public List<Info> items;
            public float mainStateAddValue;
        }

        //Symbol

        public TypeData GetTypeData(ClientEnum.Item target)
        {
            return itemTypeDatas.Find(x => x.target == target);
        }

        public Info GetItem(BaseItem item)
        {
            if (item.Type == ClientEnum.Item.None)
            {
                return new None();
            }

            return (itemTypeDatas.Find(x => x.target == item.Type)).items.Find(x => x.id == item.ID);
        }

        public Info GetRandom(ClientEnum.Item target)
        {
            TypeData typeData = GetTypeData(target);

            return typeData.items[Random.Range(0, typeData.items.Count)];
        }

        public List<ClientEnum.State> GetRandomOption(ClientEnum.Item target)
        {
            TypeData typeData = GetTypeData(target);

            return typeData.randomTarget;
        }
    }

    public partial class Item
    {
    }

    public partial class Items
    {
    }

}
