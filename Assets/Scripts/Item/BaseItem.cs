using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using JsonClass;
using System;
using ClientEnum;
using UnityEngine.Purchasing;

[System.Serializable]
public class BaseItem 
{
    [SerializeField] protected string id;
    [SerializeField] protected string index;
    [SerializeField] protected string itemName;
    [SerializeField] protected int lv;
    [SerializeField] protected int reinforce;
    [SerializeField] protected ClientEnum.Grade grade;
    [SerializeField] protected ClientEnum.Item type;
    [SerializeField] protected Datas.Pair<ClientEnum.State, float> mainState;
    [SerializeField] protected List<Datas.Pair<ClientEnum.State, (ClientEnum.Grade grade, float num)>> options;
    [SerializeField] protected Sprite sprite;

    public Sprite GetSprite => sprite;
    public int Level => lv;
    public string Name => itemName;
    public string ID => id;
    public ClientEnum.Grade Grade => grade;
    public ClientEnum.Item Type => type;
    public Datas.Pair<ClientEnum.State,float> MainState => mainState;
    public List<Datas.Pair<ClientEnum.State, (ClientEnum.Grade grade, float value)>> Options => options;

    public virtual void Set(Items info,ClientEnum.Grade target)
    {
        reinforce = 0;
        id = System.Guid.NewGuid().ToString();
        index = info.id;
        lv = DataManager.Instance.GetInfo.CurrentLevel;
        grade = target;
        sprite = info.Sprite();
        itemName = info.GetLocal();
        mainState = new Datas.Pair<ClientEnum.State, float>(info.MainState(),ScriptableManager.Instance.Get<OptionScriptable>(ScriptableType.Option).GetData(info.MainState()).Value(grade));
        options = ScriptableManager.Instance.Get<OptionScriptable>(ScriptableType.Option).GetRandomOption(ScriptableManager.Instance.Get<ItemDataScriptable>(ScriptableType.ItemData).GetOptions(type),grade);
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

    public float GetStateValue(ClientEnum.State state)
    {
        float value = 0;

        if (mainState.key == state)
        {
            value += mainState.value;
        }

        foreach (var item in options)
        {
            if (item.key == state)
            {
                value += item.value.num;
            }
        }

        return value;
    }
}
