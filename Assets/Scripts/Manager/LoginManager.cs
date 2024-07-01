using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginManager : Singleton<LoginManager>
{
    [SerializeField,ReadOnly] ClientEnum.LoginType loginType;

    protected override void Awake()
    {
    }

    void Start()
    {
        Init();
        DataManager.Instance.Init();
    }

    void Init()
    {
        string login = PlayerPrefs.GetString("LoginType");

        switch (login)
        {
            case "Google":
                loginType = ClientEnum.LoginType.Google; 
                break;
            case "Guest":
                loginType = ClientEnum.LoginType.Guest;
                break;
            default:
                break;
        }

        SetLogin();
    }

    void SetLogin()
    {
        switch (loginType)
        {
            case ClientEnum.LoginType.None:
                break;
            case ClientEnum.LoginType.Google:
                break;
            case ClientEnum.LoginType.Guest:
                break;
            default:
                break;
        }
    }

    public void OnClickScreen()
    {
        SceneManager.LoadScene(1);
    }
}
