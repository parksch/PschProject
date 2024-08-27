using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GPGS : MonoBehaviour
{
    [SerializeField] string id;

    System.Action successAction;
    System.Action failAction;

    public void GoogleInit(System.Action success,System.Action fail)
    {
        successAction = success;
        failAction = fail;

        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }

    public void GoogleSignin()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessorArchitecture);
    }

    internal void ProcessorArchitecture(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            string name = PlayGamesPlatform.Instance.GetUserDisplayName();
            id = PlayGamesPlatform.Instance.GetUserId();
            string imgUrl = PlayGamesPlatform.Instance.GetUserImageUrl();

            Debug.Log("Success \n" + name + "\n" + id + "\n" + imgUrl);
            successAction?.Invoke();
        }
        else
        {
            Debug.Log("Fail Authenticate");
            failAction.Invoke();
        }
    }
}
