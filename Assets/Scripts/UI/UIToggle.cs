using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIToggle : MonoBehaviour
{
    [SerializeField] Toggle target;
    [SerializeField] Text offText;
    [SerializeField] Text onText;
    [SerializeField] GameObject offObject;
    [SerializeField] GameObject onObject;

    public void OnClickToggle()
    {
        if (target.isOn)
        {
            if (offText != null)
            {
                offText.gameObject.SetActive(false);
            }
            if (offObject != null)
            {
                offObject.SetActive(false);
            }

            if (onText != null)
            {
                onText.gameObject.SetActive(true);
            }
            if (onObject != null)
            {
                onObject.SetActive(true);
            }
        }
        else
        {
            if (onText != null)
            {
                onText.gameObject.SetActive(false);
            }
            if (onObject != null)
            {
                onObject.SetActive(false);
            }

            if (offText != null)
            {
                offText.gameObject.SetActive(true);
            }
            if (offObject != null)
            {
                offObject.SetActive(true);
            }
        }
    }

}
