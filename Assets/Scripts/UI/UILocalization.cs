using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILocalization : MonoBehaviour
{
    [SerializeField] string textKey;

    private void Awake()
    {
        GetComponent<Text>().text = TableManager.Instance.TextScriptable.Get(textKey);
    }
}
