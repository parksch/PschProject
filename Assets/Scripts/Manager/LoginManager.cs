using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class LoginManager : Singleton<LoginManager>
{
    [SerializeField, ReadOnly] string deviceNum;

    protected override void Awake()
    {
        base.Awake();
        deviceNum = SystemInfo.deviceUniqueIdentifier;
    }

    public void OnButtonLogin()
    {
        
    }
}
