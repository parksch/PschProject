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

    [System.Serializable]
    public class ItemTypeData
    {
        public ClientEnum.Item target;
        public List<ItemData> items;
    }


}
