using JsonClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : BaseCharacter
{
    [SerializeField] UIHP hpbar;
    [SerializeField] GameObject hitObject;
    [SerializeField] bool isBoss;
    [SerializeField] string poolName;

    public bool IsBoss => isBoss;

    EnemyState State => state as EnemyState;

    public void Enqueue()
    {
        hitObject.SetActive(false);
        PoolManager.Instance.Enqueue(poolName, gameObject);
    }

    public override void Init()
    {
        base.Init();
    }

    public override void DeathAction()
    {
        base.DeathAction();
        Enqueue();
        GameManager.Instance.RemoveDeathEnemy(this);

        if (isBoss)
        {
            GameManager.Instance.StageSuccess();
        }
    }

    public override UNBigStats Attack()
    {
        return State.Attack;
    }

    public override UNBigStats HP()
    {
        return State.HP;
    }

    public override UNBigStats Defense()
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

    public override float DrainLife()
    {
        return State.DrainLife;
    }

    public void SystemDeath()
    {
        curHp.SetZero();
        Death();
        agent.StateMachine.ChangeState(AiStateID.Death);
    }

    public override void Death()
    {
        base.Death();
        hpbar.ReleaseHpBar();
        hpbar = null;
        GameManager.Instance.OnEnemyDeath(this);
        if (isBoss)
        {
            GameManager.Instance.BossDeath();
        }
    }

    public override void AttackAction()
    {
        base.AttackAction();
    }


    public override UNBigStats Hit(UNBigStats attack)
    {
        if (IsDeath)
        {
            return UNBigStats.Zero;
        }

        UNBigStats result = base.Hit(attack);

        hitObject.SetActive(false);
        hitObject.SetActive(true);

        if (isBoss)
        {
            UIManager.Instance.UpdateBossHp(curHp);
        }

        if (!IsDeath)
        {
            hpbar.SetHPRatio(curHp / HP());
        }

        UIManager.Instance.WorldCanvas.SetDamageText(transform.position,result);

        return result;
    }

    public void SetState(MonsterData monsterData,int stage,bool _isBoss = false)
    {
        poolName = monsterData.name;
        isBoss = _isBoss;
        State.Set(monsterData,isBoss, stage);

        if (isBoss)
        {
            UIManager.Instance.SetBossUI(State.HP, ScriptableManager.Instance.Get<LocalizationScriptable>(ScriptableType.Localization).Get(monsterData.local));
        }

        hpbar = UIManager.Instance.WorldCanvas.SetHpBar(this);
    }

}
