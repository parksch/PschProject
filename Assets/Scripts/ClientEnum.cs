namespace ClientEnum
{
    public enum Goods
    {
        None = 0,
        Gold,
        Scrap,
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
        None,
        HP,
        Attack,
        Defense,
        HpRegen,
        DrainLife,
        AttackRange,
        AttackSpeed,
        MoveSpeed,
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
        UltraRare,
        Max
    }

    public enum ChangeType
    {
        Sum,
        Product
    }
} 