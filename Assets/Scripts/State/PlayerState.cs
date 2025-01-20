using ClientEnum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerState : BaseCharacterState
{
    [SerializeField] float hpRegen;
    [SerializeField] float hpRegenTimer;

    public float HpRegen => hpRegen;
    public float HpRegenTimer => hpRegenTimer;

    public void UpdateState()
    {
        hp.SetZero();
        attack.SetZero();
        defense.SetZero();

        DataManager dm = DataManager.Instance;
        PlayerState playerState = dm.PlayerDefaultState;

        hp += playerState.HP;
        hp += (int)(dm.GetStateData(State.HP, ChangeType.Sum));
        hp *= dm.GetStateData(State.HP, ChangeType.Product);

        attack += playerState.Attack;
        attack += (int)dm.GetStateData(State.Attack, ChangeType.Sum);
        attack *= dm.GetStateData(State.Attack, ChangeType.Product);

        defense += playerState.Defense;
        defense += (int)dm.GetStateData(State.Defense, ChangeType.Sum);
        defense *= dm.GetStateData(State.Defense, ChangeType.Product);

        attackRange = ((playerState.AttackRange + dm.GetStateData(State.AttackRange, ChangeType.Sum)) * dm.GetStateData(State.AttackRange, ChangeType.Product));
        attackSpeed = ((playerState.AttackSpeed + dm.GetStateData(State.AttackSpeed, ChangeType.Sum)) * dm.GetStateData(State.AttackSpeed, ChangeType.Product));
        moveSpeed = ((playerState.MoveSpeed + dm.GetStateData(State.MoveSpeed, ChangeType.Sum)) * dm.GetStateData(State.MoveSpeed, ChangeType.Product));
        hpRegen = ((playerState.HpRegen + dm.GetStateData(State.HpRegen, ChangeType.Sum)) * dm.GetStateData(State.HpRegen, ChangeType.Product));
        hpRegenTimer = ((playerState.HpRegenTimer + dm.GetStateData(State.HpRegenTimer, ChangeType.Sum)) * dm.GetStateData(State.HpRegenTimer, ChangeType.Product));
        drainLife = ((playerState.drainLife + dm.GetStateData(State.DrainLife, ChangeType.Sum)) * dm.GetStateData(State.DrainLife, ChangeType.Product));

        UIManager.Instance.Get<StatePanel>().UpdateState();
    }
}