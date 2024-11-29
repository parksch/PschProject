using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISkillSelectSlot : MonoBehaviour
{
    [SerializeField] int targetIndex;
    [SerializeField] Image icon;
    [SerializeField] GameObject frontLock;

    public void Set()
    {

    }

    public void OnClick()
    {
        UIManager.Instance.Get<SkillSelectPanel>().SetSkill(targetIndex);
    }

}
