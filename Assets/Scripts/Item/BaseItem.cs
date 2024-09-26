using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseItem 
{
    [SerializeField] protected string id;
    [SerializeField] protected string index;
    [SerializeField] protected int lv;
    [SerializeField] protected ClientEnum.Grade grade;
    [SerializeField] protected ClientEnum.Item type;
    [SerializeField] protected long value;

    public string ID => id;
    public ClientEnum.Item Type => type;

    public virtual void Set(ItemScriptable.Info info,ClientEnum.Grade target)
    {
        grade = target;
    }

    public BaseItem()
    {
        lv = DataManager.Instance.GetInfo.CurrentLevel;
    }

    public static BaseItem Create(ClientEnum.Item item)
    {
        switch (item)
        {
            case ClientEnum.Item.Helmet:
                return new HelmetItem();
            case ClientEnum.Item.Armor:
                return new ArmorItem();
            case ClientEnum.Item.Weapon:
                return new WeaponItem();
            default:
                return null;
        }
    }
}
