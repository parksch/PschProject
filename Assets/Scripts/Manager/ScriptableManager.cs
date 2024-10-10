using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JsonClass;
public class ScriptableManager : Singleton<ScriptableManager>
{
    [SerializeField] DrawScriptable drawScriptable;
    [SerializeField] ItemScriptable itemScriptable;
    [SerializeField] LocalizationScriptable localizationScriptable;
    [SerializeField] OptionProbabilityScriptable optionProbabilityScriptable;
    [SerializeField] OptionScriptable optionScriptable;
    [SerializeField] ObjectScriptable objectScriptable;
    [SerializeField] StageScriptable stageScriptable;
    [SerializeField] UpgradeScriptable upgradeScriptable;

    public OptionProbabilityScriptable OptionProbabilityScriptable => optionProbabilityScriptable;
    public OptionScriptable OptionScriptable => optionScriptable;
    public DrawScriptable DrawScriptable => drawScriptable;
    public ItemScriptable ItemScriptable => itemScriptable;
    public ObjectScriptable ObjectScriptable => objectScriptable;
    public StageScriptable StageScriptable => stageScriptable;
    public UpgradeScriptable UpgradeScriptable => upgradeScriptable;
    public LocalizationScriptable LocalizationScriptable => localizationScriptable;
}

