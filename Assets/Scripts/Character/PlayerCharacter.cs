using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : BaseCharacter
{
    float currentRegenTimer = 0;

    [SerializeField] PlayerState State => state as PlayerState;

    private void FixedUpdate()
    {
        if (agent.CurrentState == AiStateID.Death)
        {
            return;
        }

        currentRegenTimer += Time.fixedDeltaTime;

        if (currentRegenTimer >= State.HpRegenTimer)
        {
            currentRegenTimer = 0;
            curHp += State.HpRegen;

            if (curHp > HP())
            {
                curHp = HP();
            }

            UIManager.Instance.OnChangeHP();
        }
    }

    public override void Init()
    {
        State.Set();
        currentRegenTimer = 0;
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
        List<EnemyCharacter> enemies = GameManager.Instance.Enemies;

        for (int i = 0; i < enemies.Count; i++)
        {
            float dist = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(enemies[i].transform.position.x, 0, enemies[i].transform.position.z));
            Vector3 normal = (new Vector3(enemies[i].transform.position.x, 0, enemies[i].transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z)).normalized;


            if (dist <= state.AttackRange && Vector3.Dot(transform.forward, normal) > 0)
            {
                enemies[i].Hit(Attack());
            }
        }
    }

    public override void Death()
    {

    }

    public override void Hit(long attack)
    {
        if (curHp <= 0)
        {
            return;
        }

        curHp -= attack;

        if (curHp < 0)
        {
            curHp = 0;
        }

        UIManager.Instance.OnChangeHP();

        if (curHp <= 0)
        {
            Death();
            agent.StateMachine.ChangeState(AiStateID.Death);
        }
    }

    public override void AnimationSpeedSet()
    {
        base.AnimationSpeedSet();
        animator.SetFloat("MoveSpeed", MoveSpeed()/DataManager.Instance.PlayerDefaultState.MoveSpeed);
    }
}
