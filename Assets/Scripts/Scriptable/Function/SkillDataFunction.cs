using ClientEnum;
using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class SkillDataScriptable // This Class is a functional Class.
    {
        public SkillData GetDataInGrade(ClientEnum.Grade target)
        {
            List<SkillData> list = skillData.FindAll(x => x.Grade() == target);

            return list[Random.Range(0,list.Count)];
        }
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
            value = (int)defaultValuesScriptable.Get("SkillPiece");

            return value;
        }

        public State State()
        {
            return (State)targetState;
        }
    }

    public partial class SkillBuffs // This Class is a functional Class.
    {
        public ChangeType ChangeType()
        {
            return (ChangeType)changeType;
        }

        public CharacterType CharacterType() 
        {
            return (CharacterType)characterType;
        }
    }

}
