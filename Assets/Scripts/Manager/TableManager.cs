using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class TableManager : Singleton<TableManager>
{
    [SerializeField,ReadOnly] ItemScriptable itemScriptable;
    [SerializeField,ReadOnly] ObjectScriptable objectScriptable;
    [SerializeField,ReadOnly] StageScriptable stageScriptable;
    [SerializeField,ReadOnly] UpgradeScriptable upgradeScriptable;

    public ItemScriptable ItemScriptable => itemScriptable;
    public ObjectScriptable ObjectScriptable => objectScriptable;
    public StageScriptable StageScriptable => stageScriptable;
    public UpgradeScriptable UpgradeScriptable => upgradeScriptable;

}
