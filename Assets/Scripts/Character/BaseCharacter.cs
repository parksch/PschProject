using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClientEnum;

public class BaseCharacter : MonoBehaviour
{
    [SerializeField] protected CharacterType characterType;
    [SerializeField] protected AiAgent agent;
    [SerializeField] protected Animator animator;
    [SerializeField] protected BaseCharacterState state;
    [SerializeField, ReadOnly] protected BaseCharacter target;
    [SerializeField] protected long curHp;

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

    public float AttackRange => state.AttackRange;

    public virtual void AttackAction()
    {
        if (Dist() <= state.AttackRange && IsLookAt)
        {
            Target().Hit(Attack());
        }
    }

    public virtual void DeathAction()
    {

    }

    public virtual void Hit(long attack)
    {
    }

    public virtual void Death()
    {
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

        return Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(Target().transform.position.x, 0, Target().transform.position.z));
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

}
