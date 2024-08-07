using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISkillSlot : MonoBehaviour
{
    [SerializeField] int index;
    [SerializeField] Image icon;
    [SerializeField,ReadOnly] float setCoolTime;
    [SerializeField,ReadOnly] float currentCoolTime;


}
