using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiInteraction : MonoBehaviour
{
    [SerializeField] Animator ani;
    [SerializeField] BaseCharacter character;

    public Animator Animator => ani;  

    public virtual void Init(AiStateMachine stateMachine)
    {
    }
}
