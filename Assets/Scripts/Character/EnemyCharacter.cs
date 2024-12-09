using JsonClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : BaseCharacter
{
    [SerializeField] GameObject hitObject;
    [SerializeField] bool isBoss;
    [SerializeField] string poolName;
    EnemyState State => state as EnemyState;

    public override void Init()
    {
        base.Init();
    }

    public override void DeathAction()
    {
        hitObject.SetActive(false);
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
        GameManager.Instance.OnEnemyDeath(this);
        base.Death();
    }

    public override void AttackAction()
    {
        base.AttackAction();
    }


    public override void Hit(long attack)
    {
        if (curHp <= 0)
        {
            return;
        }

        hitObject.SetActive(false);
        hitObject.SetActive(true);

        attack = DefenseCalculate(attack);

        curHp -= attack;

        if (curHp < 0)
        {
            curHp = 0;
        }

        if (isBoss)
        {
            UIManager.Instance.UpdateBossHp(curHp);
        }

        if (curHp <= 0)
        {
            Death();
            agent.StateMachine.ChangeState(AiStateID.Death);
        }
    }

    public void SetState(MonsterData monsterData,bool _isBoss = false)
    {
        poolName = monsterData.name;
        isBoss = _isBoss;
        State.Set(monsterData, isBoss);

        if (isBoss)
        {
            UIManager.Instance.SetBossUI(State.HP, ScriptableManager.Instance.Get<LocalizationScriptable>(ScriptableType.Localization).Get(monsterData.local));
        }
    }

}
