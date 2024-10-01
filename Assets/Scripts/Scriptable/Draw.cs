using System.Collections.Generic;
[System.Serializable]
public class Draw
{
    public int key;
    public Value value;
    [System.Serializable]
    public class Value
    {
        public string nameStringKey;
        public List<Datas> datas;
        [System.Serializable]
        public class Datas
        {
            public List<Probabilities> probabilities;
            [System.Serializable]
            public class Probabilities
            {
                public int grade;
                public int value;
            }

            public int target;
            public int goods;
            public string descKey;
            public string nameKey;
            public int needValue;
            public int limit;
            public int maxLevel;
            public int grade;
        }

    }

}
