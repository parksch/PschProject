using ClientEnum;
using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class SkillDataScriptable // This Class is a functional Class.
    {
    }

    public partial class SkillData // This Class is a functional Class.
    {
        public GameObject Prefab()
        {
            return PoolManager.Instance.Dequeue(ObjectType.Skill,prefab);
        }

        public Sprite Sprite()
        {
            return ResourcesManager.Instance.GetSprite(atlas, sprite);
        }

        public ClientEnum.Grade Grade()
        {
            return (ClientEnum.Grade)grade;
        }

        public int GetPiece()
        {
            int value = 0;
            DefaultValuesScriptable defaultValuesScriptable = ScriptableManager.Instance.Get<DefaultValuesScriptable>(ScriptableType.DefaultValues);

            switch (Grade())
            {
                case ClientEnum.Grade.Normal:
                    value = (int)defaultValuesScriptable.Get("NormalSkillPiece");
                    break;
                case ClientEnum.Grade.Rare:
                    value = (int)defaultValuesScriptable.Get("RareSkillPiece");
                    break;
                case ClientEnum.Grade.SuperRare:
                    value = (int)defaultValuesScriptable.Get("SuperRareSkillPiece");
                    break;
                case ClientEnum.Grade.UltraRare:
                    value = (int)defaultValuesScriptable.Get("UltraRareSkillPiece");
                    break;
                default:
                    break;
            }

            return value;
        }
    }

}
