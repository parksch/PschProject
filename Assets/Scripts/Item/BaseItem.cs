using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JsonClass;

[System.Serializable]
public class BaseItem 
{
    [SerializeField] protected string id = "";
    [SerializeField] protected string index = "";
    [SerializeField] protected string itemName = "";
    [SerializeField] protected int lv = 0;
    [SerializeField] protected int reinforce = 0;
    [SerializeField] protected ClientEnum.Grade grade;
    [SerializeField] protected ClientEnum.Item type;
    [SerializeField] protected Datas.Pair<ClientEnum.State, float> mainState;
    [SerializeField] protected List<Datas.Pair<ClientEnum.State, (ClientEnum.Grade grade, float num)>> options;
    [SerializeField] protected Sprite sprite;

    public void AddReinforce() => reinforce++;
    public Sprite GetSprite => sprite;
    public int Level => lv;
    public int Reinforce => reinforce;
    public string Name => itemName;
    public string ID => id;
    public ClientEnum.Grade Grade => grade;
    public ClientEnum.Item Type => type;
    public Datas.Pair<ClientEnum.State,float> MainState => mainState;
    public List<Datas.Pair<ClientEnum.State,(ClientEnum.Grade grade, float value)>> Options => options;

    public virtual void Set(Items info,ClientEnum.Grade target,int level = -1)
    {
        reinforce = 0;
        id = System.Guid.NewGuid().ToString();
        index = info.id;
        lv = level == -1 ? DataManager.Instance.CurrentLevel : level;
        grade = target;
        sprite = info.Sprite();
        itemName = info.GetLocal();
        mainState = new Datas.Pair<ClientEnum.State, float>(info.MainState(),ScriptableManager.Instance.Get<OptionScriptable>(ScriptableType.Option).GetData(info.MainState()).Value(grade));
        options = ScriptableManager.Instance.Get<OptionScriptable>(ScriptableType.Option).GetRandomOption(ScriptableManager.Instance.Get<ItemDataScriptable>(ScriptableType.ItemData).GetOptions(type),grade);
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

    public void Disassembly()
    {

    }

    void ResetItem()
    {
        id = "";
        index = "";
        itemName = "";
        lv = 0;
        reinforce = 0;
        grade = ClientEnum.Grade.Normal;
        type = ClientEnum.Item.None;
        mainState.key = ClientEnum.State.None;
        mainState.value = 0;
        options.Clear();
        sprite = null;
    }
}
