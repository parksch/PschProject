using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseCharacterState : MonoBehaviour
{
    [SerializeField] protected long  attack;     
    [SerializeField] protected long  hp;         
    [SerializeField] protected long  defense;    
    [SerializeField] protected float attackRange;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected float moveSpeed;  

    public long Attack => attack;
    public long HP => hp;
    public long Defense => defense;
    public float AttackSpeed => attackSpeed;
    public float MoveSpeed => moveSpeed;
    public float AttackRange => attackRange;
}
