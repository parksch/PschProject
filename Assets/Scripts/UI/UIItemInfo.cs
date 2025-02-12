using Datas;
using JsonClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItemInfo : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] Text itemName;
    [SerializeField] Text mainState;
    [SerializeField] Text option;

    BaseItem target;

    public void SetItem(BaseItem item)
    {
        target = item;
        icon.sprite = target.GetSprite;
        itemName.text = $"<color={ResourcesManager.Instance.GradeColor(target.Grade)}>{target.Reinforce}+ {target.Name}</color>";
        string state = ScriptableManager.Instance.Get<LocalizationScriptable>(ScriptableType.Localization).Get(target.MainState.key.ToString());
        mainState.text = $"{state} : {target.MainState.value}";
        List<Pair<ClientEnum.State, ItemOption >> options = target.Options;

        if (options.Count > 0)
        {
            option.text = string.Empty;

            for (int i = 0; i < options.Count; i++)
            {
                state = ScriptableManager.Instance.Get<LocalizationScriptable>(ScriptableType.Localization).Get(options[i].key.ToString());
                option.text += $"<color={ResourcesManager.Instance.GradeColor(options[i].value.Grade)}>{state} : {options[i].value.OptionSet}%</color> \n";
            }
        }
        else
        {
            option.text = "No Option";
        }
    }

    public void OnClickEquip()
    {
        DataManager.Instance.SetEquipItem(target);
    }

    public void OnClickReinforce()
    {
        target.AddReinforce();
        SetItem(target);
    }
}
