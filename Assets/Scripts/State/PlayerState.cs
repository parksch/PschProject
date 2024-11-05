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

        hp = (long)((playerState.HP + dm.GetStateData(ClientEnum.State.HP,ClientEnum.ChangeType.Sum)) * dm.GetStateData(ClientEnum.State.HP,ClientEnum.ChangeType.Product));
        attack = (long)((playerState.Attack + dm.GetStateData(ClientEnum.State.Attack, ClientEnum.ChangeType.Sum)) * dm.GetStateData(ClientEnum.State.Attack, ClientEnum.ChangeType.Product));
        defense = (long)((playerState.Defense + dm.GetStateData(ClientEnum.State.Defense, ClientEnum.ChangeType.Sum)) * dm.GetStateData(ClientEnum.State.Defense, ClientEnum.ChangeType.Product));
        attackRange = ((playerState.AttackRange + dm.GetStateData(ClientEnum.State.AttackRange, ClientEnum.ChangeType.Sum)) * dm.GetStateData(ClientEnum.State.AttackRange, ClientEnum.ChangeType.Product));
        attackSpeed = ((playerState.AttackSpeed + dm.GetStateData(ClientEnum.State.AttackSpeed, ClientEnum.ChangeType.Sum)) * dm.GetStateData(ClientEnum.State.AttackSpeed, ClientEnum.ChangeType.Product));
        moveSpeed = ((playerState.MoveSpeed + dm.GetStateData(ClientEnum.State.MoveSpeed, ClientEnum.ChangeType.Sum)) * dm.GetStateData(ClientEnum.State.MoveSpeed, ClientEnum.ChangeType.Product));
        hpRegen = (int)((playerState.HpRegen + dm.GetStateData(ClientEnum.State.HpRegen, ClientEnum.ChangeType.Sum)) * dm.GetStateData(ClientEnum.State.HpRegen, ClientEnum.ChangeType.Product));
        hpRegenTimer = ((playerState.HpRegenTimer + dm.GetStateData(ClientEnum.State.HpRegenTimer, ClientEnum.ChangeType.Sum)) * dm.GetStateData(ClientEnum.State.HpRegenTimer, ClientEnum.ChangeType.Product));
    }
}

