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
        DataManager.Skill skill = DataManager.Instance.EquipSkill[targetIndex];
        
        if (skill.data == null || skill.data.id == "")
        {
            icon.gameObject.SetActive(false);
            frontLock.SetActive(true);
        }
        else
        {
            icon.gameObject.SetActive(true);
            frontLock.SetActive(false);
            icon.sprite = skill.data.Sprite();
        }
    }

    public void OnClick()
    {
        UIManager.Instance.Get<SkillSelectPanel>().SetSkill(targetIndex);
    }

}
