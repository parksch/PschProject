using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiInteraction : MonoBehaviour
{
    [SerializeField] Animator ani;
    [SerializeField] BaseCharacter character;

    protected Dictionary<string,System.Action> actions = new Dictionary<string, System.Action> ();

    public System.Action GetAction(string name) => actions[name];
    public bool IsChaseRange => character.Dist() < character.AttackRange * 0.8f;
    public bool IsRange => character.Dist() < character.AttackRange;
    public bool IsAttackRange => character.Dist() < character.AttackRange;
    public bool IsTarget => character.IsTarget;
    public bool IsLookAt => character.IsLookAt;
    public float MoveSpeed => character.MoveSpeed();
    public float Dot => character.Dot();

    public BaseCharacter Character => character;
    public Transform TargetTransform => character.TargetTransform();
    public Animator Animator => ani;

    public virtual void Init(AiStateMachine stateMachine)
    {

    }

    protected virtual void AddAction()
    {

    }
}
