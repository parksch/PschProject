using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEscafe : MonoBehaviour
{
    [SerializeField, ReadOnly] CommonPanel common;
    [SerializeField] string escafeDesc;
    public void OnClickButton()
    {
        common.SetYesNo(escafeDesc, null, StageGameChange);
        UIManager.Instance.AddPanel(common);
    }

    void StageGameChange()
    {
        GameManager.Instance.OnChangeGameMode(ClientEnum.GameMode.Stage);
    }

}
