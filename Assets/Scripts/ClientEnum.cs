namespace ClientEnum
{
    public enum Goods
    {
        None = 0,
        Gold,
        Scrap,
        Gem,
        Reinforce,
        Amplification,
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
        Buff,
        Enemy,
        Map,
        Skill,
        UI
    }

    public enum DrawValue
    {
        Item,
        Skill,
    }

    public enum Item
    {
        None = 0,
        Helmet,
        Armor,
        Weapon,
        Max,
        Skill
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
        HpRegenTimer,
        Max,
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
        Gem,
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

    public enum Reward
    {
        Item,
        Goods,
    }

    public enum GameMode
    {
        Stage,
        Boss
    }
} 