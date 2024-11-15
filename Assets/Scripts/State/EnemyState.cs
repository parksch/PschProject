using JsonClass;
using ClientEnum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : BaseCharacterState
{
    public void Set(MonsterData monsterData,bool isBoss)
    {
        StageOptionScriptable option = ScriptableManager.Instance.Get<StageOptionScriptable>(ScriptableType.StageOption);
        int stage = DataManager.Instance.GetInfo.Stage;

        transform.localScale = Vector3.one * (isBoss ? option.multiplyBossSize : 1f);
        attack = (long)((monsterData.attack * option.State(stage,State.Attack)) * (isBoss ? option.multiplyBossHp : 1));
        hp = (long)((monsterData.hp * option.State(stage,State.HP)) * (isBoss ? option.multiplyBossHp : 1));
        defense = (long)((monsterData.defense * option.State(stage,State.Defense)) * (isBoss ? option.multiplyBossDefense : 1));
        attackRange = monsterData.attackRange;
        attackSpeed = monsterData.attackSpeed;
        moveSpeed = monsterData.moveSpeed;
    }
}
