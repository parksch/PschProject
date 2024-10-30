using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.WSA;
using JsonClass;
public class UIDrawSlot : MonoBehaviour
{
    [SerializeField, ReadOnly] ClientEnum.Draw type;
    [SerializeField, ReadOnly] List<Shops> datas = new List<Shops>();
    [SerializeField, ReadOnly] Text title;

    public bool isMin => currentIndex - 1 < 0;
    public bool isMax => currentIndex + 1 >= datas.Count;

    public string Title => title.text;

    public void ResetIndex() { currentIndex = 0; }
    public void AddIndex() { currentIndex = (currentIndex + 1) % datas.Count; }
    public void SubtractIndex() { currentIndex = (currentIndex - 1 > 0 ? currentIndex - 1 : 0); }

    int currentIndex = 0;

    public void Set(ClientEnum.Draw shop,string titleKey,List<Shops> _datas)
    {
        type = shop;
        datas = _datas;
        title.text = ScriptableManager.Instance.Get<LocalizationScriptable>(ScriptableType.Localization).Get(titleKey);
        currentIndex = 0;
    }

    public Shops GetCurrentData => datas[currentIndex];
}
