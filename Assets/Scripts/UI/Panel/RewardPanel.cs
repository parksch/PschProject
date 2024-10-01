using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardPanel : BasePanel
{
    public override void OnUpdate()
    {

    }

    public override void FirstLoad()
    {

    }

    public void AddItem()
    {

    }

    public override void Close()
    {
        gameObject.SetActive(false);
    }

    public override void Open()
    {
        gameObject.SetActive(true);
    }

}
