using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class ResourcesManager : Singleton<ResourcesManager>
{
    [SerializeField] Color common;
    [SerializeField] Color uncommon;
    [SerializeField] Color rare;
    [SerializeField] Color epic;
    [SerializeField] Color legendary;
    [SerializeField] Color mythical;  
    [SerializeField, ReadOnly] string spriteAtlasPath = "SpriteAtlas";
    Dictionary<string, SpriteAtlas> spriteAtlasDict = new Dictionary<string, SpriteAtlas>();

    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    void Init()
    {
        var spriteAtlasList = Resources.LoadAll<SpriteAtlas>(spriteAtlasPath);

        foreach (var atlas in spriteAtlasList)
        {
            spriteAtlasDict[atlas.name] = atlas;
        }
    }

    public Sprite GetSprite(string atlas, string sprite) => spriteAtlasDict[atlas].GetSprite(sprite);

    public Sprite GetGoodsSprite(ClientEnum.Goods goods)
    {
        return spriteAtlasDict["Goods"].GetSprite(ClientEnum.EnumString<ClientEnum.Goods>.ToString(goods));
    }

    public string GradeColor(ClientEnum.Grade grade)
    {
        Color target = Color.white;

        switch (grade)
        {
            case ClientEnum.Grade.Common:
                target = common;
                break;
            case ClientEnum.Grade.Uncommon:
                target = uncommon;
                break;
            case ClientEnum.Grade.Rare:
                target = rare;
                break;
            case ClientEnum.Grade.Epic:
                target = epic;
                break;
            case ClientEnum.Grade.Legendary:
                target = legendary;
                break;
            case ClientEnum.Grade.Mythical:
                target = mythical;
                break;
            case ClientEnum.Grade.Max:
                break;
            default:
                break;
        }

        int r = Mathf.RoundToInt(target.r * 255);
        int g = Mathf.RoundToInt(target.g * 255);
        int b = Mathf.RoundToInt(target.b * 255);

        return $"#{r:X2}{g:X2}{b:X2}";
    }

    public string GradeColorText(ClientEnum.Grade grade,string text)
    {
        return $"<color={GradeColor(grade)}>{text}</color>";
    }
}
