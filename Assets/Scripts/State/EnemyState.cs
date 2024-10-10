using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : BaseCharacterState
{
    public void Set(string code)
    {
        attack = ScriptableManager.Instance.ObjectScriptable.GetObject(code).attack;
        hp = ScriptableManager.Instance.ObjectScriptable.GetObject(code).hp;
        defense = ScriptableManager.Instance.ObjectScriptable.GetObject(code).defense;
        attackRange = ScriptableManager.Instance.ObjectScriptable.GetObject(code).attackRange;
        attackSpeed = ScriptableManager.Instance.ObjectScriptable.GetObject(code).attackSpeed;
        moveSpeed = ScriptableManager.Instance.ObjectScriptable.GetObject(code).moveSpeed;
    }
}
