using ClientEnum;
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

        hp = (long)((playerState.HP + dm.GetStateData(State.HP,ChangeType.Sum)) * dm.GetStateData(State.HP,ChangeType.Product));
        attack = (long)((playerState.Attack + dm.GetStateData(State.Attack, ChangeType.Sum)) * dm.GetStateData(State.Attack, ChangeType.Product));
        defense = (long)((playerState.Defense + dm.GetStateData(State.Defense, ChangeType.Sum)) * dm.GetStateData(State.Defense, ChangeType.Product));
        attackRange = ((playerState.AttackRange + dm.GetStateData(State.AttackRange, ChangeType.Sum)) * dm.GetStateData(State.AttackRange, ChangeType.Product));
        attackSpeed = ((playerState.AttackSpeed + dm.GetStateData(State.AttackSpeed, ChangeType.Sum)) * dm.GetStateData(State.AttackSpeed, ChangeType.Product));
        moveSpeed = ((playerState.MoveSpeed + dm.GetStateData(State.MoveSpeed, ChangeType.Sum)) * dm.GetStateData(State.MoveSpeed, ChangeType.Product));
        hpRegen = (int)((playerState.HpRegen + dm.GetStateData(State.HpRegen, ChangeType.Sum)) * dm.GetStateData(State.HpRegen, ChangeType.Product));
        hpRegenTimer = ((playerState.HpRegenTimer + dm.GetStateData(State.HpRegenTimer, ChangeType.Sum)) * dm.GetStateData(State.HpRegenTimer, ChangeType.Product));
        drainLife = ((playerState.drainLife + dm.GetStateData(State.DrainLife, ChangeType.Sum)) * dm.GetStateData(State.DrainLife, ChangeType.Product));
    }
}