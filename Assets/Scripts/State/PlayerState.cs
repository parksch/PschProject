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

    public void Set()
    {
        hp = DataManager.Instance.PlayerDefaultState.HP;
        attack = DataManager.Instance.PlayerDefaultState.Attack;
        defense = DataManager.Instance.PlayerDefaultState.Defense;
        attackRange = DataManager.Instance.PlayerDefaultState.AttackRange;
        attackSpeed = DataManager.Instance.PlayerDefaultState.AttackSpeed;
        moveSpeed = DataManager.Instance.PlayerDefaultState.MoveSpeed;
        hpRegen = DataManager.Instance.PlayerDefaultState.HpRegen;
        hpRegenTimer = DataManager.Instance.PlayerDefaultState.HpRegenTimer;
    }
}

