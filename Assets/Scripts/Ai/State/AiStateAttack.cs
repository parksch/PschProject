using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateAttack : AiState
{
    public void Enter(AiAgent agent)
    {
        agent.Interaction.Animator.SetBool("Attack", true);
        agent.transform.LookAt(agent.Interaction.TargeTransform);
        agent.AiAnimationEvent.SetMid(agent.Interaction.GetAction("Attack"));
        agent.AiAnimationEvent.SetEnd(() =>
        {
            if (!agent.Interaction.IsRange)
            {
                agent.StateMachine.ChangeState(AiStateID.Idle);
            }
        });
    }

    public void Exit(AiAgent agent)
    {
        agent.Interaction.Animator.SetBool("Attack", false);
    }

    public AiStateID GetID()
    {
        return AiStateID.Attack;
    }

    public void Update(AiAgent agent)
    {

    }
}
