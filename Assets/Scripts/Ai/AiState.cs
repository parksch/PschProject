using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AiStateID
{
    Idle,
    Chase,
    Attack,
    Death
}

public interface AiState 
{
    public AiStateID GetID();
    public void Update(AiAgent agent);
    public void Enter(AiAgent agent);
    public void Exit(AiAgent agent);
}
