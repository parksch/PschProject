using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AiStateMachine 
{
    [SerializeField] AiStateID currentState;

    public AiStateID CurrentState => currentState;

    AiAgent agent;
    AiState[] states;

    public AiStateMachine(AiAgent agent)
    {
        int numStates = System.Enum.GetNames(typeof(AiStateID)).Length;
        states = new AiState[numStates];
        this.agent = agent;
    }

    public void RegisterState(AiState state)
    {
        int index = (int)state.GetID();
        states[index] = state;
    }

    public AiState GetState(AiStateID stateId)
    {
        int index = (int)stateId;
        return states[index];
    }

    public void Update()
    {
        GetState(currentState)?.Update(agent);
    }

    public void ChangeState(AiStateID newState)
    {
        GetState(currentState)?.Exit(agent);
        currentState = newState;
        GetState(currentState)?.Enter(agent);
    }
}
