using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillNormal : SkillBase
{
    public override void Active(Transform target)
    {
        base.Active(target);

        for (int i = 0; i < buffs.Count; i++)
        {
            if (character.CharacterType == buffs[i].CharacterType())
            {
                character.AddBuff(buffs[i].state, buffs[i].timer, buffs[i].value, buffs[i].ChangeType());
            }
        }
    }

    protected override void Attack(BaseCharacter enemy, float target)
    {
        base.Attack(enemy, target);

        if (enemy.IsDeath)
        {
            return;
        }

        for (int i = 0; i < buffs.Count; i++)
        {
            if (enemy.CharacterType == buffs[i].CharacterType())
            {
                enemy.AddBuff(buffs[i].state, buffs[i].timer, buffs[i].value, buffs[i].ChangeType());
            }
        }
    }

    protected override void Attack(BaseCharacter enemy, UNBigStats stats)
    {
        base.Attack(enemy, stats);

        if (enemy.IsDeath)
        {
            return;
        }

        for (int i = 0; i < buffs.Count; i++)
        {
            if (enemy.CharacterType == buffs[i].CharacterType())
            {
                enemy.AddBuff(buffs[i].state, buffs[i].timer, buffs[i].value, buffs[i].ChangeType());
            }
        }
    }

}
