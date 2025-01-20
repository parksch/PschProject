using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseCharacterState : MonoBehaviour
{
    [SerializeField] protected BigStats  attack;     
    [SerializeField] protected BigStats  hp;         
    [SerializeField] protected BigStats  defense;    
    [SerializeField] protected float attackRange;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float drainLife;

    public BigStats Attack => attack;
    public BigStats HP => hp;
    public BigStats Defense => defense;
    public float DrainLife => drainLife;
    public float AttackSpeed => attackSpeed;
    public float MoveSpeed => moveSpeed;
    public float AttackRange => attackRange;
}
