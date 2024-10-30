using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class ItemScriptable // This Class is a functional Class.
    {
        public ClientEnum.Item GetRandomTarget()
        {
            return (ClientEnum.Item)item[Random.Range(0, item.Count)].target;
        }

        public class None : Items
        {

        }

        Item GetTargetData(ClientEnum.Item target)
        {
            return item.Find(x => (ClientEnum.Item)x.target == target);
        }

        public Items GetRandom(ClientEnum.Item target)
        {
            Item typeData = GetTargetData(target);

            return typeData.items[Random.Range(0, typeData.items.Count)];
        }

        public List<ClientEnum.State> GetOptions(ClientEnum.Item target)
        {
            Item typeData = GetTargetData(target);
            return typeData.TargetStates();
        }
    }

    public partial class Item
    {
        public List<ClientEnum.State> TargetStates()
        {
            List<ClientEnum.State> states = new List<ClientEnum.State>();

            for (int i = 0; i < randomTarget.Count; i++)
            {
                states.Add((ClientEnum.State)randomTarget[i]);
            }

            return states;
        }
    }

    public partial class Items
    {
        public ClientEnum.State MainState()
        {
            return (ClientEnum.State)mainState;
        }
    }

}
