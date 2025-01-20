using JsonClass;
using ClientEnum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : BaseCharacterState
{
    public void Set(MonsterData monsterData,bool isBoss,int stage)
    {
        attack.SetZero();
        hp.SetZero();
        defense.SetZero();

        StageOptionScriptable option = ScriptableManager.Instance.Get<StageOptionScriptable>(ScriptableType.StageOption);

        transform.localScale = Vector3.one * (isBoss ? option.multiplierBossSize : 1f);

        attack += monsterData.attack;
        attack *= option.State(stage, State.Attack);
        attack *= (isBoss ? option.multiplierBossHp : 1);

        hp += monsterData.hp;
        hp *= option.State(stage, State.HP);
        hp *= (isBoss ? option.multiplierBossHp : 1);

        defense += monsterData.defense;
        defense *= option.State(stage, State.Defense);
        defense *= (isBoss ? option.multiplierBossDefense : 1);

        attackRange = monsterData.attackRange;
        attackSpeed = monsterData.attackSpeed;
        drainLife = monsterData.drainLife;
        moveSpeed = monsterData.moveSpeed;
    }
}
