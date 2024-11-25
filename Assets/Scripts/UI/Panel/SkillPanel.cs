using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : BasePanel
{
    [SerializeField] Image icon;
    [SerializeField] Text skillName;
    [SerializeField] Text lvPiece;
    [SerializeField] Text desc;
    [SerializeField] UIButton upgrade;
    [SerializeField] UIButton equip;

    public override void OnUpdate()
    {
    }

    public override void FirstLoad()
    {

    }

    public override void Open()
    {
        base.Open();
    }

    public override void Close()
    {

    }
}
