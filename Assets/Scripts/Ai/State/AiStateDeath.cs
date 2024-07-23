using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateDeath : AiState
{
    bool isEnd = false;
    float deathTime = 1f;
    float currentTime = 0;

    public void Enter(AiAgent agent)
    {
        isEnd = false;
        currentTime = 0;

        agent.Interaction.Animator.Play("Death", -1, 0f);
        agent.AiAnimationEvent.SetEnd(() => { isEnd = true; });
    }

    public void Exit(AiAgent agent)
    {
        agent.AiAnimationEvent.ResetEvent();
    }

    public AiStateID GetID()
    {
        return AiStateID.Death;
    }

    public void Update(AiAgent agent)
    {
        if (!isEnd)
        {
            return;
        }

        currentTime += Time.deltaTime;

        if (currentTime > deathTime)
        {
            agent.Interaction.GetAction("Death")();
            isEnd = false;
        }
    }
}
