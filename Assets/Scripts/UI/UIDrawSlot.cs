using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JsonClass;
public class UIDrawSlot : MonoBehaviour
{
    [SerializeField, ReadOnly] ClientEnum.Draw type;
    [SerializeField, ReadOnly] List<Shops> dates = new List<Shops>();
    [SerializeField, ReadOnly] Text title;

    public bool isMin => currentIndex - 1 < 0;
    public bool isMax => currentIndex + 1 >= dates.Count;

    public string Title => title.text;

    public void ResetIndex() { currentIndex = 0; }
    public void AddIndex() { currentIndex = (currentIndex + 1) % dates.Count; }
    public void SubtractIndex() { currentIndex = (currentIndex - 1 > 0 ? currentIndex - 1 : 0); }

    int currentIndex = 0;

    public void Set(ClientEnum.Draw shop,string titleKey,List<Shops> _dates)
    {
        type = shop;
        dates = _dates;
        title.text = ScriptableManager.Instance.Get<LocalizationScriptable>(ScriptableType.Localization).Get(titleKey);
        currentIndex = 0;
    }

    public Shops GetCurrentData => dates[currentIndex];
}
