using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class BaseItem 
{
    [SerializeField] protected string id;
    [SerializeField] protected string index;
    [SerializeField] protected int lv;
    [SerializeField] protected ClientEnum.Grade grade;
    [SerializeField] protected ClientEnum.Item type;
    [SerializeField] protected Datas.Pair<ClientEnum.State, float> mainState;
    [SerializeField] protected List<Datas.Pair<ClientEnum.State, float>> options;

    public string ID => id;
    public ClientEnum.Item Type => type;

    public virtual void Set(ItemScriptable.Info info,ClientEnum.Grade target)
    {
        lv = DataManager.Instance.GetInfo.CurrentLevel;
        grade = target;
        mainState = new Datas.Pair<ClientEnum.State, float>(info.MainState,TableManager.Instance.OptionScriptable.GetData(info.MainState).Value(grade));
        options = TableManager.Instance.OptionScriptable.GetRandomOption(TableManager.Instance.ItemScriptable.GetRandomOption(type),grade);
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
