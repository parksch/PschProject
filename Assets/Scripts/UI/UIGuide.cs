using ClientEnum;
using JsonClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGuide : MonoBehaviour
{
    [SerializeField, ReadOnly] GuideData current;
    [SerializeField, ReadOnly] Image item;
    [SerializeField, ReadOnly] Text title;
    [SerializeField, ReadOnly] Text desc;

    public void SetGuide(GuideData guideData)
    {

    }

    public void CheckGuide(GuideType guideType,string code,int value)
    {

    }

    public void OnClick()
    {

    }
}
