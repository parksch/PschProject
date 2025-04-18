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
        GoldDungeonTicket,
        GemDungeonTicket,
        Money,
        Max,
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
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary,
        Mythical,
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
        Boss,
        GoldDungeon,
        GemDungeon,
    }

    public enum GuideType
    {
        Number,
        Action
    }

    public enum GuideKey
    {
        Upgrade,
        Equip,
        Draw,
    }

    public enum ContentLockType
    {
        Level,
        Guide
    }
}