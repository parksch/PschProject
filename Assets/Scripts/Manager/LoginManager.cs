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
        signUp.Init(loginType);
    }

    public void OnClickScreen()
    {
        SceneManager.LoadScene(1);
    }
}
