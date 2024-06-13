using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance = null;
    public static T Instance
    {
        get
        {
            // SingletonBehaviour가 초기화 되기 전이라면            
            if (_instance == null)
            {
                // 해당 오브젝트를 찾아 할당한다.                
                _instance = FindObjectOfType<T>(true);

                if (_instance == null)
                {
                    _instance = new GameObject(typeof(T).ToString()).AddComponent<T>();
                }

            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
