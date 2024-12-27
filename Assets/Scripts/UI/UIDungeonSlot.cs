using JsonClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDungeonSlot : MonoBehaviour
{
    DungeonsData target;

    public void SetData(DungeonsData dungeonsData)
    {
        target = dungeonsData;
    }
}
