using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiPlayerInterraction : AiInteraction
{
    public override void Init(AiStateMachine stateMachine)
    {
        stateMachine.RegisterState(new AiStateIdle());
        stateMachine.RegisterState(new AiStateAttack());
        stateMachine.RegisterState(new AiStateChase());
        stateMachine.RegisterState(new AiStateDeath());
        AddAction();
    }

    protected override void AddAction()
    {
        actions["Attack"] = () =>
        {
            Character.AttackAction();
        };

        actions["Death"] = () =>
        {
            Character.DeathAction();
        };
    }
}
