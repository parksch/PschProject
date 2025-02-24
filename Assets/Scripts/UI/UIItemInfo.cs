using ClientEnum;
using Datas;
using JsonClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItemInfo : MonoBehaviour
{
    [SerializeField,ReadOnly] Image icon;
    [SerializeField,ReadOnly] Text itemName;
    [SerializeField,ReadOnly] Text mainState;
    [SerializeField,ReadOnly] Text optionName;
    [SerializeField,ReadOnly] Text optionState;
    [SerializeField,ReadOnly] UIButton reinforceButton;
    [SerializeField,ReadOnly] UIGuideUpdater guideUpdater;

    BaseItem target;

    public void SetItem(BaseItem item)
    {
        target = item;
        icon.sprite = target.GetSprite;
        itemName.text = $"<color={ResourcesManager.Instance.GradeColor(target.Grade)}>{target.Name} +{target.Reinforce}</color>";

        JsonClass.LocalizationScriptable local = ScriptableManager.Instance.Get<JsonClass.LocalizationScriptable>(ScriptableType.Localization);
        string state = local.Get(ClientEnum.EnumString<ClientEnum.State>.ToString(target.MainState.key));

        mainState.text = "";
        mainState.text += $"<color={ResourcesManager.Instance.GradeColor(target.Grade)}>{local.Get(ClientEnum.EnumString<ClientEnum.Grade>.ToString(target.Grade))}</color> \n";
        mainState.text += $"{state} : {target.MainState.value.OptionSet}";

        List<Pair<ClientEnum.State, ItemOption >> options = target.Options;

        if (options.Count > 0)
        {
            optionName.text = string.Empty;
            optionState.text = string.Empty;

            for (int i = 0; i < options.Count; i++)
            {
                state = string.Format(local.Get("Add"), local.Get(options[i].key.ToString()));
                optionName.text += $"<color={ResourcesManager.Instance.GradeColor(options[i].value.Grade)}>{state}</color> \n";
                optionState.text += $"<color={ResourcesManager.Instance.GradeColor(options[i].value.Grade)}>{options[i].value.OptionSet}%</color> \n";
            }
        }
        else
        {
            optionName.text = "";
            optionState.text = "";
        }
    }

    public void OnClickEquip()
    {
        DataManager.Instance.SetEquipItem(target);
        guideUpdater.SetCode(EnumString<ClientEnum.Item>.ToString(target.Type));
    }

    public void OnClickReinforce()
    {
        DefaultValuesScriptable defaultValues = ScriptableManager.Instance.Get<DefaultValuesScriptable>(ScriptableType.DefaultValues);
        float needValue = defaultValues.Get("NeedReinforce");
        DataManager.Instance.UseGoods(ClientEnum.Goods.Reinforce, (int)needValue);
        target.AddReinforce();
        SetItem(target);
    }

    public void ReinforceUpdate()
    {
        DefaultValuesScriptable defaultValues = ScriptableManager.Instance.Get<DefaultValuesScriptable>(ScriptableType.DefaultValues);
        float needValue = defaultValues.Get("NeedReinforce");
        float checkMax = defaultValues.Get("MaxReinforce");
        reinforceButton.SetInterractable(DataManager.Instance.CheckGoods(ClientEnum.Goods.Reinforce, (int)needValue) && target.Reinforce < checkMax);
    }
}
