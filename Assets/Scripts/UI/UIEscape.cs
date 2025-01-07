using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEscape : MonoBehaviour
{
    [SerializeField, ReadOnly] CommonPanel common;
    [SerializeField] string escapeDesc;
    public void OnClickButton()
    {
        common.SetYesNo(escapeDesc, null, StageGameChange);
        UIManager.Instance.AddPanel(common);
    }

    void StageGameChange()
    {
        GameManager.Instance.OnChangeGameMode(ClientEnum.GameMode.Stage);
    }

}
