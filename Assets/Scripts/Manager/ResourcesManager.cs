using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class ResourcesManager : Singleton<ResourcesManager>
{
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
}
