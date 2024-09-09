using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.WSA;

public class UIShopSlot : MonoBehaviour
{
    [SerializeField, ReadOnly] ClientEnum.Shop type;
    [SerializeField, ReadOnly] List<ShopScriptable.Data> datas = new List<ShopScriptable.Data>();
    [SerializeField, ReadOnly] Text title;

    public string Title => title.text;

    public void Set(ClientEnum.Shop shop,string titleKey,List<ShopScriptable.Data> _datas)
    {
        type = shop;
        datas = _datas;
        title.text = TableManager.Instance.TextScriptable.Get(titleKey);
    }

}
