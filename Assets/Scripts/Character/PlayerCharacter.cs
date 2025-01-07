using ClientEnum;
using JsonClass;
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
        SetAnimationSpeed();
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
            AddHp(State.HpRegen);
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
        return (long)(State.Attack * BuffValues(ClientEnum.State.Attack));
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

    public override float DrainLife()
    {
        return State.DrainLife;
    }

    public override void AttackAction()
    {
        List<EnemyCharacter> enemies = GameManager.Instance.Enemies;

        for (int i = 0; i < enemies.Count; i++)
        {
            float dist = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(enemies[i].transform.position.x, 0, enemies[i].transform.position.z));
            Vector3 normal = (new Vector3(enemies[i].transform.position.x, 0, enemies[i].transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z)).normalized;

            if (dist <= AttackRange + enemies[i].Size && Vector3.Dot(transform.forward, normal) > 0)
            {
                long attack = (long)(enemies[i].Hit(Attack()) * DrainLife());

                if (!IsDeath)
                {
                    AddHp(attack);
                }
            }
        }
    }

    public override void DeathAction()
    {
        GameManager.Instance.StageFail();
    }

    public void ResetPlayer()
    {
        curHp = HP();
        ResetBuff();
        ResetAI();
        UIManager.Instance.OnChangePlayerHP(GetHPRatio);
        UIManager.Instance.ResetBuff();
    }

    public override void Death()
    {
        if (agent.CurrentState == AiStateID.Skill)
        {
            current.Stop();
        }

        base.Death();
        UIManager.Instance.ResetBuff();
    }

    public override long Hit(long attack)
    {
        if (curHp <= 0)
        {
            return 0;
        }

        attack = base.Hit(attack);
        UIManager.Instance.OnChangePlayerHP(GetHPRatio);

        return attack;
    }

    public override void SetAnimationSpeed()
    {
        base.SetAnimationSpeed();
        animator.SetFloat("MoveSpeed", MoveSpeed()/DataManager.Instance.PlayerDefaultState.MoveSpeed);
    }

    public void SetSkill(int index,DataManager.Skill target)
    {

        if (agent.CurrentState == AiStateID.Skill)
        {
            current.Stop();
            ResetAI();
            SetPlayerPos(GameManager.Instance.PlayerStart);
        }
        else if (agent.CurrentState != AiStateID.Death)
        {
            ResetAI();
            SetPlayerPos(GameManager.Instance.PlayerStart);
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

    public void SetPlayerPos(Vector3 pos)
    {
        transform.position = pos;
    }

    protected override BuffBase SetBuff(BuffData buffData, float timer, float value)
    {
        BuffBase buff = base.SetBuff(buffData, timer, value);

        if (buff != null)
        {
            UIManager.Instance.AddBuff(buff, timer, value);
        }

        return buff;
    }

    void ResetAI()
    {
        agent.StateMachine.ChangeState(AiStateID.Idle);
        target = null;
    }
}
