using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class PlayerCharacter : BaseCharacter
{
    [SerializeField] List<Skill> skillList = new List<Skill>();
    [SerializeField,ReadOnly] Transform skillTrans;
    [SerializeField,ReadOnly] PlayableDirector director;

    PlayerState State => state as PlayerState;

    float currentRegenTimer = 0;

    [System.Serializable]
    public class Skill
    {
        public string name;
        public TimelineAsset asset;
        public float currentTime;
    }

    public void StateUpdate()
    {
        State.UpdateState();
        AnimationSpeedSet();
        UIManager.Instance.OnChangePlayerHP(GetHPRatio);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

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

            UIManager.Instance.OnChangePlayerHP(GetHPRatio);
        }
    }

    public override void Init()
    {
        DataManager.Instance.OnChangeSkill += SetSkill;
        State.UpdateState();
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

    public override void DeathAction()
    {
        transform.position = Vector3.zero;
        curHp = HP();
        UIManager.Instance.OnChangePlayerHP(GetHPRatio);
        agent.StateMachine.ChangeState(AiStateID.Idle);
        target = null;
    }

    public override void Death()
    {
        GameManager.Instance.StageFail();
    }

    public override void Hit(long attack)
    {
        if (curHp <= 0)
        {
            return;
        }

        attack = DefenseCalculate(attack);

        curHp -= attack;

        if (curHp < 0)
        {
            curHp = 0;
        }

        UIManager.Instance.OnChangePlayerHP(GetHPRatio);

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

    public void SetSkill(int index,DataManager.Skill skillData)
    {

    }

    public void ActiveSkill()
    {

    }
}
