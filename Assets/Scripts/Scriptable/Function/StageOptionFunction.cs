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
                    return stage * multiplierStageHp;
                case ClientEnum.State.Attack:
                    return stage * multiplierStageAttack;
                case ClientEnum.State.Defense:
                    return stage * multiplierStageDefense;
                default:
                    return 1;
            }
        }
    }

    public partial class StageMonsters // This Class is a functional Class.
    {
    }

}
