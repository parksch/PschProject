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
        hp = (long)(DataManager.Instance.PlayerDefaultState.HP + DataManager.Instance.GetUpgradeValue(ClientEnum.State.HP,ClientEnum.ChangeType.Sum));
        attack = (long)(DataManager.Instance.PlayerDefaultState.Attack + DataManager.Instance.GetUpgradeValue(ClientEnum.State.Attack, ClientEnum.ChangeType.Sum));
        defense = (long)(DataManager.Instance.PlayerDefaultState.Defense + DataManager.Instance.GetUpgradeValue(ClientEnum.State.Defense, ClientEnum.ChangeType.Sum));
        attackRange = DataManager.Instance.PlayerDefaultState.AttackRange + DataManager.Instance.GetUpgradeValue(ClientEnum.State.AttackRange, ClientEnum.ChangeType.Sum);
        attackSpeed = DataManager.Instance.PlayerDefaultState.AttackSpeed + DataManager.Instance.GetUpgradeValue(ClientEnum.State.AttackSpeed,ClientEnum.ChangeType.Sum);
        moveSpeed = DataManager.Instance.PlayerDefaultState.MoveSpeed + DataManager.Instance.GetUpgradeValue(ClientEnum.State.MoveSpeed, ClientEnum.ChangeType.Sum);
        hpRegen = (int)(DataManager.Instance.PlayerDefaultState.HpRegen + DataManager.Instance.GetUpgradeValue(ClientEnum.State.HpRegen, ClientEnum.ChangeType.Sum));
        hpRegenTimer = DataManager.Instance.PlayerDefaultState.HpRegenTimer;
    }
}

