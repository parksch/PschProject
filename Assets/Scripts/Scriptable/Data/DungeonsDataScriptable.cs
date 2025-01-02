using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class DungeonsDataScriptable : ScriptableObject
    {
        public List<DungeonsData> dungeonsData = new List<DungeonsData>();
    }

    [System.Serializable]
    public partial class DungeonsData
    {
        public int gamemode;
        public int maxLevel;
        public int targetAddStage;
        public int needGoodsType;
        public int itemType;
        public string atlas;
        public string sprite;
        public string nameLocal;
        public string descriptionLocal;
        public string mapPrefab;
        public List<string> monsters;
        public List<DungeonReward> dungeonReward;
    }

    [System.Serializable]
    public partial class DungeonReward
    {
        public int index;
        public int value;
        public int changeType;
        public float addValue;
    }
}
