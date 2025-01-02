using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiGoldBossInteraction : AiInteraction
{
    public override void Init(AiStateMachine stateMachine)
    {
        stateMachine.RegisterState(new AiStateStay());
        stateMachine.RegisterState(new AiStateDeath());
        AddAction();
    }

    protected override void AddAction()
    {
        actions["Death"] = () =>
        {
            Character.DeathAction();
        };
    }
}
