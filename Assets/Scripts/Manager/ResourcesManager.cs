using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class ResourcesManager : Singleton<ResourcesManager>
{
    [SerializeField] Color normal;
    [SerializeField] Color rare;
    [SerializeField] Color superRare;
    [SerializeField] Color ultraRare;
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
        switch (goods)
        {
            case ClientEnum.Goods.None:
                return null;
            case ClientEnum.Goods.Scrap:
                return spriteAtlasDict["Goods"].GetSprite("Scrap");
            case ClientEnum.Goods.Gold:
                return spriteAtlasDict["Goods"].GetSprite("Gold");
            case ClientEnum.Goods.Gem:
                return spriteAtlasDict["Goods"].GetSprite("Gem");
            case ClientEnum.Goods.Money:
                return null;
            default:
                return null;
        }
    }

    public string GradeColor(ClientEnum.Grade grade)
    {
        Color target = Color.white;

        switch (grade)
        {
            case ClientEnum.Grade.Normal:
                target = normal;
                break;
            case ClientEnum.Grade.Rare:
                target = rare;
                break;
            case ClientEnum.Grade.SuperRare:
                target = superRare;
                break;
            case ClientEnum.Grade.UltraRare:
                target = ultraRare;
                break;
            default:
                target = normal;
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
