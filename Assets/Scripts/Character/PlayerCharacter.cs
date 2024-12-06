using ClientEnum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class PlayerCharacter : BaseCharacter
{
    [SerializeField,ReadOnly] List<SkillBase> skillList = new List<SkillBase>();
    [SerializeField,ReadOnly] Transform skillTrans;
    [SerializeField,ReadOnly] PlayableDirector director;

    SkillBase current;
    PlayerState State => state as PlayerState;
    float currentRegenTimer = 0;

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

        if (agent.CurrentState != AiStateID.Skill)
        {
            for (int i = 0; i < UIManager.Instance.SkillSlots.Count; i++)
            {
                if (UIManager.Instance.SkillSlots[i].IsActiveOn && Dist() < UIManager.Instance.SkillSlots[i].Range)
                {
                    ActiveSkill(i);
                }
            }
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
        curHp = HP();
        UIManager.Instance.OnChangePlayerHP(GetHPRatio);
        ResetAIAndPos();
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
            if (agent.CurrentState == AiStateID.Skill)
            {
                current.Stop();
            }

            Death();
            agent.StateMachine.ChangeState(AiStateID.Death);
        }
    }

    public override void AnimationSpeedSet()
    {
        base.AnimationSpeedSet();
        animator.SetFloat("MoveSpeed", MoveSpeed()/DataManager.Instance.PlayerDefaultState.MoveSpeed);
    }

    public void SetSkill(int index,DataManager.Skill target)
    {
        if (agent.CurrentState == AiStateID.Skill)
        {
            current.Stop();
            ResetAIAndPos();
        }
        else if (agent.CurrentState != AiStateID.Death)
        {
            ResetAIAndPos();
        }

        List<DataManager.Skill> list = DataManager.Instance.EquipSkill;

        if (skillList[index] != null && skillList[index].ID == target.data.id)
        {
            return;
        }

        for (int i = 0; i < skillList.Count; i++)
        {
            if (skillList[i] != null)
            {
                bool check = false;
                foreach (var item in list)
                {
                    if (item != null && item.data != null && item.data.id == skillList[i].ID)
                    {
                        check = true;
                        break;
                    }
                }

                if (!check)
                {
                    GameObject push = skillList[i].gameObject;
                    PoolManager.Instance.Enqueue(push.name, push);
                    skillList[i] = null;
                }
            }
        }

        SkillBase skill = skillList.Find(x => x != null && x.ID == target.data.id);

        if (skill == null)
        {
            skill = target.data.Prefab().GetComponent<SkillBase>();
            skill.transform.parent = GameManager.Instance.SkillParent;
            skill.transform.position = Vector3.zero;
            skill.SetSkill(this);
            skill.gameObject.SetActive(false);
            skillList[index] = skill;
        }
        else
        {
            if (skill != skillList[index])
            {
                SkillBase temp = skillList[index];
                int oldIndex = skillList.FindIndex(x => x != null && x.ID == target.data.id);

                skillList[index] = skillList[oldIndex];
                skillList[oldIndex] = temp;
            }
        }

    }

    public void ActiveSkill(int index)
    {
        UIManager.Instance.SkillSlots[index].ResetSkill();
        agent.StateMachine.ChangeState(AiStateID.Skill);
        current = skillList[index];
        current.Active(transform);
    }

    public override float GetState(State target)
    {
        float value = 0;

        switch (target)
        {
            case ClientEnum.State.HP:
                value = HP();
                break;
            case ClientEnum.State.Attack:
                value = Attack();
                break;
            case ClientEnum.State.Defense:
                value = Defense();
                break;
            case ClientEnum.State.HpRegen:
                value = State.HpRegen;
                break;
            default:
                break;
        }

        return value;
    }

    void ResetAIAndPos()
    {
        transform.position = Vector3.zero;
        agent.StateMachine.ChangeState(AiStateID.Idle);
        target = null;
    }

}
