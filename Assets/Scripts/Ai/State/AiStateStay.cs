using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateStay : AiState
{
    public void Enter(AiAgent agent)
    {
        agent.Interaction.Animator.Play("Idle", -1, 0f);
    }

    public void Exit(AiAgent agent)
    {
    }

    public AiStateID GetID()
    {
       return AiStateID.Idle;
    }

    public void Update(AiAgent agent)
    {
    }
}
