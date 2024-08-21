using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISignUp : MonoBehaviour
{
    public void Init(ClientEnum.LoginType loginType)
    {
        switch (loginType)
        {
            case ClientEnum.LoginType.None:

                gameObject.SetActive(true);
                break;
            case ClientEnum.LoginType.Google:

                break;
            case ClientEnum.LoginType.Guest:
                
                break;
            default:
                break;
        }
    }

    public void OnClickGoggle()
    {

    }

    public void OnClickGuest()
    {

    }
}
