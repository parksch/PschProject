using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAgent : MonoBehaviour
{
    [SerializeField] AiStateID initializeState;
    [SerializeField] AiStateMachine stateMachine;
    [SerializeField] AiInteraction interaction;

    private void Awake()
    {
        stateMachine = new AiStateMachine(this);
        interaction.Init(stateMachine);
        stateMachine.ChangeState(initializeState);
    }
}
