using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class ObjectDataScriptable // This Class is a functional Class.
    {
        public ObjectData Get(string key)
        {
            return objectData.Find(x => x.name == key);
        }
    }

    public partial class ObjectData
    {
        public GameObject GetGameOjbect()
        {
            return Resources.Load<GameObject>("Prefab/" + prefab);
        }

        public ClientEnum.ObjectType GetObjectType()
        {
            return (ClientEnum.ObjectType)objectType;
        }
    }

}
