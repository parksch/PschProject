using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class MonsterDataScriptable // This Class is a functional Class.
    {
        public MonsterData Get(string name) => monsterData.Find(x => x.name == name);
    }

    public partial class MonsterData // This Class is a functional Class.
    {
        public ClientEnum.ObjectType ObjectType() { return (ClientEnum.ObjectType)objectType;}
    }

}
