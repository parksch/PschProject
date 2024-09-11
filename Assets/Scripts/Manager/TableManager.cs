using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : Singleton<TableManager>
{
    [SerializeField,ReadOnly] DrawScriptable shopScriptable;
    [SerializeField,ReadOnly] ItemScriptable itemScriptable;
    [SerializeField,ReadOnly] ObjectScriptable objectScriptable;
    [SerializeField,ReadOnly] StageScriptable stageScriptable;
    [SerializeField,ReadOnly] UpgradeScriptable upgradeScriptable;
    [SerializeField,ReadOnly] TextScriptable textScriptable;

    public DrawScriptable ShopScriptable => shopScriptable;
    public ItemScriptable ItemScriptable => itemScriptable;
    public ObjectScriptable ObjectScriptable => objectScriptable;
    public StageScriptable StageScriptable => stageScriptable;
    public UpgradeScriptable UpgradeScriptable => upgradeScriptable;
    public TextScriptable TextScriptable => textScriptable;
}
