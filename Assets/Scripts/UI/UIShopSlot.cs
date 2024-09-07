using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShopSlot : MonoBehaviour
{
    [SerializeField, ReadOnly] ClientEnum.Shop type;
    [SerializeField, ReadOnly] List<ShopScriptable.Data> datas = new List<ShopScriptable.Data>();
    [SerializeField] Text title;

    public void Set(ClientEnum.Shop shop,List<ShopScriptable.Data> find)
    {

    }
}
