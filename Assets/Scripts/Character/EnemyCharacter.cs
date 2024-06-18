using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : BaseCharacter
{
    EnemyState State => state as EnemyState;
    long curHp;

    public override void Init()
    {
        curHp = HP();
        animator.SetFloat("AttackSpeed",State.AttackSpeed);
    }

    public override long Attack()
    {
        return State.Attack;
    }

    public override long HP()
    {
        return State.HP;
    }

    public override long Defense()
    {
        return State.Defense;
    }

    public override float AttackSpeed()
    {
        return State.AttackSpeed;
    }

    public override float MoveSpeed()
    {
        return State.MoveSpeed;
    }

}
