using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateAttack : AiState
{
    bool isAttack = false;
    public void Enter(AiAgent agent)
    {
        isAttack = false;
        agent.Interaction.Animator.SetBool("Attack", true);
        agent.transform.LookAt(agent.Interaction.TargeTransform);
        agent.AiAnimationEvent.SetStart(() => { isAttack = true; });
        agent.AiAnimationEvent.SetMid(agent.Interaction.GetAction("Attack"));
        agent.AiAnimationEvent.SetEnd(() => { isAttack = false; });
    }

    public void Exit(AiAgent agent)
    {
        agent.Interaction.Animator.SetBool("Attack", false);
        agent.AiAnimationEvent.ResetEvent();
    }

    public AiStateID GetID()
    {
        return AiStateID.Attack;
    }

    public void Update(AiAgent agent)
    {
        if (!agent.Interaction.IsAttackRange && !isAttack)
        {
            agent.StateMachine.ChangeState(AiStateID.Idle);
            return;
        }
    }
}
