using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateChase : AiState
{
    public void Enter(AiAgent agent)
    {
        agent.Interaction.Animator.SetBool("Move", true);
    }

    public void Exit(AiAgent agent)
    {
        agent.Interaction.Animator.SetBool("Move", false);
    }

    public AiStateID GetID()
    {
        return AiStateID.Chase;
    }

    public void Update(AiAgent agent)
    {
        if (agent.Interaction.IsChaseRange)
        {
            agent.StateMachine.ChangeState(AiStateID.Attack);
        }
        else
        {
            if(agent.Interaction.TargetTransform != null)
            {
                agent.transform.LookAt(agent.Interaction.TargetTransform);
                agent.transform.position += ((agent.Interaction.TargetTransform.position - agent.transform.position).normalized * agent.Interaction.MoveSpeed) * Time.deltaTime;
            }
            else
            {
                agent.StateMachine.ChangeState(AiStateID.Idle);
            }
        }
    }

}
