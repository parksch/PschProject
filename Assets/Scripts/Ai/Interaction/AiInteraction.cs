using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiInteraction : MonoBehaviour
{
    [SerializeField] Animator ani;
    [SerializeField] BaseCharacter character;

    protected Dictionary<string,System.Action> actions = new Dictionary<string, System.Action> ();

    public System.Action GetAction(string name) => actions[name];
    public bool IsRange => character.Dist() * 0.9f < character.AttackRange;
    public float MoveSpeed => character.MoveSpeed();
    public Transform TargeTransform => character.TargetTransform();
    public Animator Animator => ani;  

    public virtual void Init(AiStateMachine stateMachine)
    {

    }

    protected virtual void AddAction()
    {

    }
}
