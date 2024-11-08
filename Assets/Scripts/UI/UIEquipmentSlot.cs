using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEquipmentSlot : ItemSlot
{
    [SerializeField] ClientEnum.Item itemType;
    public ClientEnum.Item ItemType => itemType;
}
