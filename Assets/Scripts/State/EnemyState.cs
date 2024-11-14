using JsonClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : BaseCharacterState
{
    public void Set(MonsterData monsterData,bool isBoss)
    {
        StageOptionScriptable option = ScriptableManager.Instance.Get<StageOptionScriptable>(ScriptableType.StageOption);
        int stage = DataManager.Instance.GetInfo.Stage - 1;

        transform.localScale = Vector3.one * (isBoss ? option.multiplyBossSize : 1f);
        attack = (long)((monsterData.attack * (stage * option.multiplyPerStageHp)) * (isBoss ? option.multiplyBossHp : 1));
        hp = (long)(monsterData.hp * (stage * option.multiplyPerStageHp) * (isBoss ? option.multiplyBossHp : 1));
        defense = (long)(monsterData.defense * (stage * option.multiplyPerStageDefanse) * (isBoss ? option.multiplyBossDefense : 1));
        attackRange = monsterData.attackRange;
        attackSpeed = monsterData.attackSpeed;
        moveSpeed = monsterData.moveSpeed;
    }
}
