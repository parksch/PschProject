using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateIdle : AiState
{
    float waitTimer = 0.3f;
    float currentTimer = 0f;

    public void Enter(AiAgent agent)
    {
        currentTimer = 0f;
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
        if (currentTimer < waitTimer)
        {
            currentTimer+= Time.deltaTime;
            return;
        }

        if (agent.Interaction.IsRange)
        {
            agent.StateMachine.ChangeState(AiStateID.Attack);
        }
        else
        {
            if (agent.Interaction.IsTarget)
            {
                agent.StateMachine.ChangeState(AiStateID.Chase);
            }
        }
    }

}
