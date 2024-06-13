using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClientEnum;

public class BaseCharacter : MonoBehaviour
{
    [SerializeField] AiAgent agent;

    public virtual void Init()
    {

    }

    public virtual void Attack()
    {

    }

    public virtual void Hit(long attack)
    {

    }
}
