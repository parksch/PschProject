using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SDKManager : Singleton<SDKManager>
{
    public void Init()
    {
        ADMobInit();
    }
}
