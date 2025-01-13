using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISignUp : MonoBehaviour
{
    public void Init()
    {
        gameObject.SetActive(true);
    }

    public void OnClickGoggle()
    {
        SDKManager.Instance.GoogleInit(() => { PlayerPrefs.SetString("LoginType", "Google"); gameObject.SetActive(false); }, null);
        SDKManager.Instance.GoogleSignIn();
    }

    public void OnClickGuest()
    {
        PlayerPrefs.SetString("LoginType", "Guest");
        DataManager.Instance.SetDevice(SystemInfo.deviceUniqueIdentifier);
        gameObject.SetActive(false);
    }
}
