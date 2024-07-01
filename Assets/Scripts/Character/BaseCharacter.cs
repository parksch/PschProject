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
    [SerializeField,ReadOnly] protected BaseCharacter target;

    BaseCharacter Target()
    {
        if (target == null)
        {
            target = GameManager.Instance.GetTarget(characterType);
        }

        return target;
    }

    public bool IsTarget => target != null;

    public virtual void Init()
    {

    }

    public float AttackRange => state.AttackRange;

    public virtual void AttackAction()
    {
        if (Dist() <= state.AttackRange && Vector3.Dot(new Vector3(Target().transform.position.x, 0, Target().transform.position.z), new Vector3(transform.position.x, 0, transform.position.z)) > 0)
        {
            Target().Hit(Attack());
        }
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

}
