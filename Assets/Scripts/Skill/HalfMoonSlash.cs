using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfMoonSlash : SkillBase
{
    public override void Active(Transform target)
    {
        base.Active(target);

        for (int i = 0; i < buffs.Count; i++)
        {
            character.AddBuff(buffs[i].state, buffs[i].timer, buffs[i].value);
        }
    }


}
