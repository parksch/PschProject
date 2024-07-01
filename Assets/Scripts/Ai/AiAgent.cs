using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAgent : MonoBehaviour
{
    [SerializeField,ReadOnly] AiStateID initializeState;
    [SerializeField,ReadOnly] AiStateMachine stateMachine;
    [SerializeField] AiInteraction interaction;
    [SerializeField] AiAnimationEvent animationEvent;

    public AiAnimationEvent AiAnimationEvent => animationEvent;
    public AiStateMachine StateMachine => stateMachine;
    public AiInteraction Interaction => interaction;

    private void Awake()
    {
        stateMachine = new AiStateMachine(this);
        interaction.Init(stateMachine);
        stateMachine.ChangeState(initializeState);
    }

    private void Update()
    {
        stateMachine.Update();
    }
}
