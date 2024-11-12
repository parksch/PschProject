using JsonClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : BaseCharacterState
{
    public void Set(MonsterData monsterData)
    {
        attack = monsterData.attack;
        hp = monsterData.hp;
        defense = monsterData.defense;
        attackRange = monsterData.attackRange;
        attackSpeed = monsterData.attackSpeed;
        moveSpeed = monsterData.moveSpeed;
    }
}
