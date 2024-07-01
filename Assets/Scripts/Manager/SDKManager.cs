using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SDKManager : Singleton<SDKManager>
{
    [SerializeField, ReadOnly] GPGS gpgs;
    [SerializeField, ReadOnly] AdMob admob;
    [SerializeField, ReadOnly] Firebase firebase;

    public GPGS GPGS => gpgs;
    public AdMob AdMob => admob;
    public Firebase Firebase => firebase;

}
