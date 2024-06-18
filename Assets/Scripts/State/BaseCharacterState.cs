using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacterState : MonoBehaviour
{
    [SerializeField] long attack;
    [SerializeField] long hp;
    [SerializeField] long defense;
    [SerializeField] float attackRange;
    [SerializeField] float attackSpeed;
    [SerializeField] float moveSpeed;

    public long Attack => attack;
    public long HP => hp;
    public long Defense => defense;
    public float AttackSpeed => attackSpeed;
    public float MoveSpeed => moveSpeed;
    public float AttackRange => attackRange;
}
