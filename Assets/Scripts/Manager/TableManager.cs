using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : Singleton<TableManager>
{
    [SerializeField, ReadOnly] DrawScriptable drawScriptable;
    [SerializeField, ReadOnly] ItemScriptable itemScriptable;
    [SerializeField, ReadOnly] ObjectScriptable objectScriptable;
    [SerializeField, ReadOnly] StageScriptable stageScriptable;
    [SerializeField, ReadOnly] UpgradeScriptable upgradeScriptable;
    [SerializeField, ReadOnly] TextScriptable textScriptable;
    [SerializeField, ReadOnly] OptionScriptable optionScriptable;

    public OptionScriptable OptionScriptable => optionScriptable;
    public DrawScriptable DrawScriptable => drawScriptable;
    public ItemScriptable ItemScriptable => itemScriptable;
    public ObjectScriptable ObjectScriptable => objectScriptable;
    public StageScriptable StageScriptable => stageScriptable;
    public UpgradeScriptable UpgradeScriptable => upgradeScriptable;
    public TextScriptable TextScriptable => textScriptable;
}
