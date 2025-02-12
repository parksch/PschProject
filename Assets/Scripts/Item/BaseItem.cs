using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseItem 
{
    [SerializeField,ReadOnly] protected string id = "";
    [SerializeField,ReadOnly] protected string local = "";
    [SerializeField,ReadOnly] protected string prefab = "";
    [SerializeField,ReadOnly] protected int lv = 0;
    [SerializeField] protected int reinforce = 0;
    [SerializeField] protected ClientEnum.Grade grade;
    [SerializeField] protected ClientEnum.Item type;
    [SerializeField] protected Datas.Pair<ClientEnum.State, ItemOption> mainState;
    [SerializeField] protected List<Datas.Pair<ClientEnum.State,ItemOption>> options;
    [SerializeField] protected Sprite sprite;

    public void AddReinforce() => reinforce++;
    public Sprite GetSprite => sprite;
    public int Level => lv;
    public int Reinforce => reinforce;    
    public string Name => "";
    public string ID => id;
    public ClientEnum.Grade Grade => grade;
    public ClientEnum.Item Type => type;
    public Datas.Pair<ClientEnum.State, ItemOption> MainState => mainState;
    public List<Datas.Pair<ClientEnum.State,ItemOption >> Options => options;

    public virtual void Set(JsonClass.GradeItem gradeItem,int level = -1)
    {
        id = System.Guid.NewGuid().ToString();
        reinforce = 0;
        lv = level == -1 ? DataManager.Instance.CurrentLevel : level;
        grade = gradeItem.Grade();
        SetResource(gradeItem.GetRandom());

        JsonClass.OptionScriptable option = ScriptableManager.Instance.Get<JsonClass.OptionScriptable>(ScriptableType.Option);
        ItemOption mainOption = new ItemOption();
        mainOption.SetValue(ClientEnum.ChangeType.Sum, (int)(gradeItem.startValue + (gradeItem.startValue * (gradeItem.mainStateAddValue * level))));

        mainState = new Datas.Pair<ClientEnum.State, ItemOption>(GetMainState(), mainOption);

        options = option.GetRandomOption(GetOptions(), grade);
    }


    public float GetStateValue(ClientEnum.State state,ClientEnum.ChangeType changeType)
    {
        float value = 0;

        if (mainState.key == state && mainState.value.ChangeType == changeType)
        {
            value += mainState.value.OptionSet;
        }

        foreach (var item in options)
        {
            if (item.key == state && item.value.ChangeType == changeType)
            {
                value += item.value.OptionSet;
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
        local = "";
        prefab = "";
        lv = 0;
        reinforce = 0;
        grade = ClientEnum.Grade.Common;
        mainState.value.Reset();
        options.Clear();
        sprite = null;
    }


    protected void SetResource(JsonClass.ResourcesItem resourcesItem)
    {
        sprite = ResourcesManager.Instance.GetSprite(GetAtlas(), resourcesItem.sprite);
        local = resourcesItem.local;
        prefab = resourcesItem.prefab;
    }

    protected List<ClientEnum.State> GetOptions()
    {
        return ScriptableManager.Instance.Get<JsonClass.ItemDataScriptable>(ScriptableType.ItemData).GetOptions(type);
    }

    protected ClientEnum.State GetMainState()
    {
        return ScriptableManager.Instance.Get<JsonClass.ItemDataScriptable>(ScriptableType.ItemData).MainState(type);
    }

    protected string GetAtlas()
    {
        return ScriptableManager.Instance.Get<JsonClass.ItemDataScriptable>(ScriptableType.ItemData).Atlas(type);
    }
}
