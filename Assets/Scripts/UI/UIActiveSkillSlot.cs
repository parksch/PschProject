using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIActiveSkillSlot : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] Image fill;
    [SerializeField] Sprite defaultSprite;
    DataManager.Skill target;
    float timer;

    public void SetSkill(DataManager.Skill skill)
    {
        target = skill;

        if (target == null || target.data == null || target.data.id == "")
        {
            timer = 0;
            icon.sprite = defaultSprite;
        }
        else
        {
            timer = target.data.coolTime;
            icon.sprite = target.data.Sprite();
        }
    }

    void FixedUpdate()
    {
        if (target == null || target.data == null || target.data.id == "")
        {
            fill.fillAmount = 0;
        }
        else
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                timer = 0;
            }

            fill.fillAmount = timer/target.data.coolTime;
        }

    }

    public void OnClick()
    {
        if (target == null || target.data == null || target.data.id == "" || timer > 0)
        {
            return;
        }
    }
}
