using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class StageOptionScriptable // This Class is a functional Class.
    {
        public float State(int stage,ClientEnum.State state)
        {
            if (stage == 1)
            {
                return 1;
            }

            switch (state)
            {
                case ClientEnum.State.HP:
                    return stage * multiplyPerStageHp;
                case ClientEnum.State.Attack:
                    return stage * multiplyPerStageAttack;
                case ClientEnum.State.Defense:
                    return stage * multiplyPerStageDefanse;
                default:
                    return 1;
            }
        }
    }

    public partial class StageMonsters // This Class is a functional Class.
    {
    }

}
