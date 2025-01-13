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

        SDKManager.Instance.ShowRewardedAD();
    }
}
