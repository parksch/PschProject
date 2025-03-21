using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAgent : MonoBehaviour
{
    [SerializeField, ReadOnly] AiStateMachine stateMachine;
    [SerializeField] AiStateID initializeState;
    [SerializeField] AiInteraction interaction;
    [SerializeField] AiAnimationEvent animationEvent;

    public AiAnimationEvent AiAnimationEvent => animationEvent;
    public AiStateMachine StateMachine => stateMachine;
    public AiInteraction Interaction => interaction;
    public AiStateID CurrentState => StateMachine.CurrentState;


    private void Awake()
    {
        stateMachine = new AiStateMachine(this);
        interaction.Init(stateMachine);
        SetInitializeState();
    }

    private void Update()
    {
        stateMachine.Update();
    }

    public void SetInitializeState()
    {
        stateMachine.ChangeState(initializeState);
    }
}
