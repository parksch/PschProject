using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseCharacterState : MonoBehaviour
{
    [SerializeField] protected UNBigStats  attack;     
    [SerializeField] protected UNBigStats  hp;         
    [SerializeField] protected UNBigStats  defense;    
    [SerializeField] protected float attackRange;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float drainLife;

    public UNBigStats Attack => attack;
    public UNBigStats HP => hp;
    public UNBigStats Defense => defense;
    public float DrainLife => drainLife;
    public float AttackSpeed => attackSpeed;
    public float MoveSpeed => moveSpeed;
    public float AttackRange => attackRange;
}
