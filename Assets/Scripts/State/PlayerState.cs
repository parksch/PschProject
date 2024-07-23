using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerState : BaseCharacterState
{
    public void Set()
    {
        hp = DataManager.Instance.HP;
        attack = DataManager.Instance.Attack;
        defense = DataManager.Instance.Defense;
        attackRange = DataManager.Instance.AttackRange;
        attackSpeed = DataManager.Instance.AttackSpeed;
        moveSpeed = DataManager.Instance.MoveSpeed;
    }
}

