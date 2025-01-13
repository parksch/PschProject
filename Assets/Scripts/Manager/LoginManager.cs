using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginManager : Singleton<LoginManager>
{
    [SerializeField,ReadOnly] ClientEnum.LoginType loginType;
    [SerializeField,ReadOnly] UISignUp signUp;
    
    protected override void Awake()
    {
    }

    void Start()
    {
        Init();
        SDKManager.Instance.Init();
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
                signUp.Init();
                break;
            case ClientEnum.LoginType.Google:
                DataManager.Instance.SetDevice(SystemInfo.deviceUniqueIdentifier);
                break;
            case ClientEnum.LoginType.Guest:
                DataManager.Instance.SetDevice(SystemInfo.deviceUniqueIdentifier);
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
