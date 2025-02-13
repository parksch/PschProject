using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalvagePanel : BasePanel
{
    [SerializeField,ReadOnly] RectTransform rect;
    [SerializeField,ReadOnly] UIGradeToggle gradeTogglePrefab;
    [SerializeField,ReadOnly] List<UIGradeToggle> uIGradeToggles = new List<UIGradeToggle>();

    public override void FirstLoad()
    {
        for (int i = 0; i < (int)ClientEnum.Grade.Max; i++)
        {
            if (i == 0)
            {
                gradeTogglePrefab.Set((ClientEnum.Grade)i);
                uIGradeToggles.Add(gradeTogglePrefab);
            }
            else
            {
                UIGradeToggle toggle = Instantiate(gradeTogglePrefab,rect).GetComponent<UIGradeToggle>();
                toggle.Set((ClientEnum.Grade)i);
                uIGradeToggles.Add(toggle);
            }
        }
    }

    public void OnClickSalvage()
    {

    }
}
