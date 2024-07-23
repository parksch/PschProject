using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : BaseCharacter
{
    [SerializeField] string poolName;
    EnemyState State => state as EnemyState;

    public override void Init()
    {
        State.Set(poolName);
        base.Init();
    }

    public override void DeathAction()
    {
        PoolManager.Instance.Enqueue(poolName, gameObject);
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

    public override void Death()
    {
        GameManager.Instance.RemoveEnemy(this);
        base.Death();
    }
}
