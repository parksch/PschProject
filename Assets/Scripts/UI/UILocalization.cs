using JsonClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILocalization : MonoBehaviour
{
    [SerializeField] string textKey;

    private void Awake()
    {
        GetComponent<Text>().text = ScriptableManager.Instance.Get<LocalizationScriptable>(ScriptableType.Localization).Get(textKey);
    }
}
