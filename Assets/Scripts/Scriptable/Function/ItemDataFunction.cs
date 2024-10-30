using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class ItemDataScriptable// This Class is a functional Class.
    {
        public ClientEnum.Item GetRandomTarget()
        {
            return (ClientEnum.Item)itemData[Random.Range(0, itemData.Count)].target;
        }

        public class None : Items
        {

        }

        ItemData GetTargetData(ClientEnum.Item target)
        {
            return itemData.Find(x => (ClientEnum.Item)x.target == target);
        }

        public Items GetRandom(ClientEnum.Item target)
        {
            ItemData typeData = GetTargetData(target);

            return typeData.items[Random.Range(0, typeData.items.Count)];
        }

        public List<ClientEnum.State> GetOptions(ClientEnum.Item target)
        {
            ItemData typeData = GetTargetData(target);
            return typeData.TargetStates();
        }

    }

    public partial class ItemData
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
