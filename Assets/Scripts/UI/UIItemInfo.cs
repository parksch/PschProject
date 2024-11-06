using Datas;
using DG.Tweening;
using JsonClass;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        itemName.text = $"<color={ResourcesManager.Instance.GradeColor(target.Grade)}> {target.Name}</color>";
        string state = ScriptableManager.Instance.Get<LocalizationScriptable>(ScriptableType.Localization).Get(target.MainState.key.ToString());
        mainState.text = $"{state} : {target.MainState.value}%";
        List<Pair<ClientEnum.State, (ClientEnum.Grade grade, float num)>> options = target.Options;

        if (options.Count > 0)
        {
            option.text = string.Empty;

            for (int i = 0; i < options.Count; i++)
            {
                state = ScriptableManager.Instance.Get<LocalizationScriptable>(ScriptableType.Localization).Get(options[i].key.ToString());
                option.text += $"<color={ResourcesManager.Instance.GradeColor(options[i].value.grade)}>{state} : {options[i].value.num}</color> \n";
            }
        }
        else
        {
            option.text = "No Option";
        }
    }

    public void OnClickEquip()
    {

        gameObject.SetActive(false);
    }

    public void OnClickReinforce()
    {

    }
}
