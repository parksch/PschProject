using ClientEnum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffHP : BuffBase
{

    public override void BuffUpdate()
    {
        switch (changeType) 
        {
            case ChangeType.Sum:
                if (isDebuff)
                {
                    character.SubHP(UNBigStats.Zero - Value(State.HP, ChangeType.Sum));
                }
                else
                {
                    character.SumHp(UNBigStats.Zero + Value(State.HP, ChangeType.Sum));
                }
                break;
            case ChangeType.Product:
                character.SumHp(character.HP() * Value(State.HP,ChangeType.Product));
                break;
        }

    }
}
