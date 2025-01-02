using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClientEnum;
using UnityEngine.Rendering;
using JsonClass;
using System;

public class BaseCharacter : MonoBehaviour
{
    [SerializeField, ReadOnly] protected List<BuffBase> buffs = new List<BuffBase>();
    [SerializeField, ReadOnly] protected BaseCharacter target;
    [SerializeField] protected Transform buffTrans;
    [SerializeField] protected AiAgent agent;
    [SerializeField] protected Animator animator;
    [SerializeField] protected BaseCharacterState state;
    [SerializeField] protected CharacterType characterType;
    [SerializeField] protected long curHp;

    public float Size => gameObject.transform.localScale.y/2f;

    public CharacterType CharacterType => characterType;

    public void SetIdle()
    {
        agent.StateMachine.ChangeState(AiStateID.Idle);
    }

    public Animator Ani => animator;

    protected virtual void FixedUpdate()
    {
        if (IsDeath)
        {
            return;
        }

        for (int i = 0; i < buffs.Count; i++)
        {
            if (buffs[i].Timer <= 0)
            {
                buffs[i].BuffEnd();
                buffs[i].Enqueue();
                buffs.RemoveAt(i);
                i--;
            }
        }
    }

    public float GetHPRatio => ((float)curHp / HP());

    BaseCharacter Target()
    {
        if (target == null || target.IsDeath)
        {
            target = GameManager.Instance.GetTarget(characterType);
        }

        return target;
    }

    public bool IsDeath => curHp <= 0;
    public bool IsTarget => Target() != null;
    public bool IsLookAt => Dot() > 0;
    public virtual void Init()
    {
        curHp = HP();
        AnimationSpeedSet();
    }

    public void SetInitializeState()
    {
        agent.SetInitializeState();
    }

    public float AttackRange => state.AttackRange + Size;

    public virtual void AttackAction()
    {
        if (Dist() <= AttackRange && IsLookAt)
        {
            Target().Hit(Attack());
        }
    }

    public virtual void DeathAction()
    {
    }

    public virtual long Hit(long attack)
    {
        if (curHp <= 0)
        {
            return 0;
        }

        attack = DefenseCalculate(attack);
        curHp -= attack;

        if (curHp < 0)
        {
            curHp = 0;
        }

        if (curHp <= 0)
        {
            Death();
            agent.StateMachine.ChangeState(AiStateID.Death);
        }

        return attack;
    }

    public virtual void Death()
    {
        BuffReset();
    }

    public virtual long Attack()
    {
        return 0;
    }

    public virtual long Defense()
    {
        return 0;
    }

    public virtual long HP()
    {
        return 0;
    }

    public virtual float AttackSpeed()
    {
        return 0;
    }

    public virtual float MoveSpeed()
    {
        return 0;
    }

    public float Dot()
    {
        if (Target() == null)
        {
            return 0;
        }

        Vector3 normal = (new Vector3(Target().transform.position.x, 0, Target().transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z)).normalized;

        return Vector3.Dot(normal, transform.forward);
    }

    public float Dist()
    {
        if (Target() == null)
        {
            return float.MaxValue;
        }

        float dist = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(Target().transform.position.x, 0, Target().transform.position.z));
        dist = dist - Target().Size;

        return dist;
    }

    public Vector3 Normal()
    {
        if (Target() == null)
        {
            return Vector3.zero;
        }

        return (new Vector3(Target().transform.position.x, 0, Target().transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z)).normalized;
    }

    public Transform TargetTransform()
    {
        if (Target() == null)
        {
            return null;
        }

        return Target().transform;
    }

    public virtual void AnimationSpeedSet()
    {
        animator.SetFloat("AttackSpeed", AttackSpeed());
    }

    protected long DefenseCalculate(long attack)
    {
        if (attack *.9f <= Defense())
        {
            attack = (long)(attack * 0.1f);
        }
        else if (attack *.7f <= Defense())
        {
            attack = (long)(attack * 0.3f);
        }
        else if (attack *.5f <= Defense())
        {
            attack = (long)(attack * 0.5f);
        }
        else if (attack *.3f <= Defense())
        {
            attack = (long)(attack * 0.7f);
        }
        else
        {
            attack = (long)(attack * 0.9f);
        }

        return attack;
    }

    public void AddBuff(string key,float timer,float addValue)
    {
        JsonClass.BuffData buffData = ScriptableManager.Instance.Get<BuffDataScriptable>(ScriptableType.BuffData).buffData.Find(x => x.name == key);
        
        if (buffData == null)
        {
            return;
        }

        SetBuff(buffData,timer,addValue);
    }
    
    public virtual float GetState(ClientEnum.State target)
    {
        return 0;
    }

    public void BuffReset()
    {
        for (int i = 0; i < buffs.Count; i++)
        {
            buffs[i].Enqueue();
        }
        buffs.Clear();
    }

    protected virtual void SetBuff(JsonClass.BuffData buffData,float timer,float value)
    {
        BuffBase buffBase = buffs.Find(x => x.ID == buffData.name);

        if (buffBase == null)
        {
            buffBase = PoolManager.Instance.Dequeue(ObjectType.Buff, buffData.name).GetComponent<BuffBase>();
            buffBase.transform.parent = buffTrans;
            buffBase.transform.localPosition = Vector3.zero;
            buffBase.transform.localRotation = Quaternion.identity;
            buffBase.BuffStart(timer, value);
            buffs.Add(buffBase);
        }
        else
        {
            buffBase.gameObject.SetActive(false);
            buffBase.gameObject.SetActive(true);
            buffBase.BuffCheck(timer, value);
        }
    }

    protected float BuffValues(ClientEnum.State target)
    {
        float value = 1;

        for (int i = 0; i < buffs.Count; i++)
        {
            value += buffs[i].Value(target);
        }

        return value;
    }
}
