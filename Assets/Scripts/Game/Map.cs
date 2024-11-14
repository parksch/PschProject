using JsonClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] Spawn spawn;

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

    public void CreateEnemy(List<string> monsters)
    {
        spawn.CreateEnemy(monsters, StageScriptable);
    }

    public void CreateBoss(string monster)
    {
        spawn.CreateBoss(monster, StageScriptable);
    }
}
