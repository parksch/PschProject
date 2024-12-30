using JsonClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDungeonSlot : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Text title;

    public DungeonsData Target => target;

    DungeonsData target;

    public void SetData(DungeonsData dungeonsData)
    {
        target = dungeonsData;
        image.sprite = target.Sprite();
        title.text = target.Title();
    }
}
