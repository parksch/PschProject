using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerState : BaseCharacterState
{
    [SerializeField] int hpRegen;
    [SerializeField] float hpRegenTimer;

    public int HpRegen => hpRegen;
    public float HpRegenTimer => hpRegenTimer;

    public void UpdateState()
    {
        DataManager dm = DataManager.Instance;
        PlayerState playerState = dm.PlayerDefaultState;

        hp = (long)(playerState.HP              );
        attack = (long)(playerState.Attack      );
        defense = (long)(playerState.Defense    );
        attackRange = playerState.AttackRange   ;
        attackSpeed = playerState.AttackSpeed   ;
        moveSpeed = playerState.MoveSpeed       ;
        hpRegen = (int)(playerState.HpRegen     );
        hpRegenTimer = playerState.HpRegenTimer;
    }
}

