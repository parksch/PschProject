using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemFactory 
{
    public static BaseItem Create(ClientEnum.Item item)
    {
        switch (item)
        {
            case ClientEnum.Item.Helmet:
                return new HelmetItem();
            case ClientEnum.Item.Armor:
                return new ArmorItem();
            case ClientEnum.Item.Weapon:
                return new WeaponItem();
            default:
                return null;
        }
    }
}
