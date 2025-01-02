using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class StageDataScriptable // This Class is a functional Class.
    {
        public StageData Get(int index) => stageData.Find(x => x.index == index);
    }

    public partial class StageData // This Class is a functional Class.
    {
        public List<(int goodsIndex, int value)> Rewards()
        {
            List<(int goodsIndex, int value)> rewards = new List<(int goodsIndex, int value)>();
            
            for (int i = 0; i < stageRewards.Count; i++)
            {
                rewards.Add((stageRewards[i].goodsIndex, stageRewards[i].value));
            }

            return rewards;
        }

        public void Monsters(List<string> strs)
        {
            for (int i = 0; i < monsters.Count; i++)
            {
                strs.Add(monsters[i]);
            }
        }
    }

    public partial class StageRewards // This Class is a functional Class.
    {
        public ClientEnum.Goods Goods() {  return (ClientEnum.Goods)goodsIndex; }
    }

}
