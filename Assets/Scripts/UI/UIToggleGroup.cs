using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ToggleGroup))]
public class UIToggleGroup : MonoBehaviour
{
    [SerializeField] ToggleGroup toggleGroup;
    [SerializeField] Toggle firstToggle;
    [SerializeField] List<Toggle> toggles;

    public void ResetToggle()
    {
        toggleGroup.allowSwitchOff = true;

        for (int i = 0; i < toggles.Count; i++)
        {
            toggles[i].isOn = false;
        }

        firstToggle.isOn = true;

        toggleGroup.allowSwitchOff = false;
    }
}
