using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : BaseCharacter
{
    [SerializeField] PlayerState State => state as PlayerState;

    public override void Init()
    {
        base.Init();
    }

    public override long Attack()
    {
        return State.Attack;
    }

    public override float AttackSpeed()
    {
        return State.AttackSpeed;
    }

    public override long HP()
    {
        return State.HP;
    }

    public override float MoveSpeed()
    {
        return State.MoveSpeed;
    }
    public override long Defense()
    {
        return State.Defense;
    }

    public override void AttackAction()
    {

    }

    public override void Death()
    {

    }

    public override void Hit(long attack)
    {

    }
}
