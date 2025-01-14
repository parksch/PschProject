using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADPanel : BasePanel
{
    public override void FirstLoad()
    {

    }

    public override void Open()
    {
        base.Open();

        SDKManager.Instance.SetAdFullScreenContentClosed(UIManager.Instance.BackPanel);

        SDKManager.Instance.ShowRewardedAD();
    }

    void AdSuccess()
    {

    }

    void AdFail()
    {

    }
}
