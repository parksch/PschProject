namespace ClientEnum
{
    public enum Goods
    {
        Scrap,
        Gold,
        Gem,
        Money,
    }

    public enum LoginType
    {
        None,
        Google,
        Guest,
    }

    public enum CharacterType
    {
        Player,
        Enemy
    }

    public enum ObjectType
    {
        Object,
        UI
    }

    public enum Item
    {
        None,
        Helmet,
        Armor,
        Weapon,
    }

    public enum State
    {
        Hp,
        Attack,
        Int,
        Defense,
        HpRegen,
    }

    public enum UpgradeType
    {
        None,
        StatePanel
    }

    public enum Language
    {
        Kor,
        En
    }

    public enum Draw
    {
        Min = 0,
        Scrap,
        Daily,
        Max
    }

    public enum Grade
    { 
        Normal,
        Rare,
        SuperRare,
        UltraRare
    }
} 