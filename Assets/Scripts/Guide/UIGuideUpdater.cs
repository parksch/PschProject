using ClientEnum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGuideUpdater : MonoBehaviour
{
    [SerializeField] GuideType guideType;
    [SerializeField] GuideKey guideKey;
    [SerializeField] string code;
    [SerializeField] int count = 1;

    public void SetCode(string _code)
    {
        code = _code;
    }

    public void OnGuideValueAdd()
    {
        UIManager.Instance.Guide.AddGuideValue(guideType, guideKey, code, count);
    }
}
