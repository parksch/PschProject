using JsonClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] Spawn spawn;
    [SerializeField] string poolName;

    StageOptionScriptable stageOptionScriptable = null;

    StageOptionScriptable StageScriptable
    {
        get
        {
            if (stageOptionScriptable == null)
            {
                stageOptionScriptable = ScriptableManager.Instance.Get<StageOptionScriptable>(ScriptableType.StageOption);
            }

            return stageOptionScriptable;
        }
    }

    public string Pool => poolName;

    public void CreateEnemy(List<string> monsters,int stage)
    {
        spawn.CreateEnemy(monsters, StageScriptable, stage);
    }

    public void CreateBoss(string monster, int stage)
    {
        spawn.CreateBoss(monster, StageScriptable, stage);
    }
}
