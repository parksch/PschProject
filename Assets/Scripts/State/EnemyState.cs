using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : BaseCharacterState
{
    public void Set(string code)
    {
        attack = TableManager.Instance.ObjectScriptable.GetObject(code).attack;
        hp = TableManager.Instance.ObjectScriptable.GetObject(code).hp;
        defense = TableManager.Instance.ObjectScriptable.GetObject(code).defense;
        attackRange = TableManager.Instance.ObjectScriptable.GetObject(code).attackRange;
        attackSpeed = TableManager.Instance.ObjectScriptable.GetObject(code).attackSpeed;
        moveSpeed = TableManager.Instance.ObjectScriptable.GetObject(code).moveSpeed;
    }
}
