using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiEnemyInteraction : AiInteraction
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

        };

        actions["Death"] = () =>
        {

        };
    }
}
