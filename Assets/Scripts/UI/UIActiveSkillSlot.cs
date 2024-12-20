using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIActiveSkillSlot : MonoBehaviour
{
    [SerializeField] int index;
    [SerializeField] Image icon;
    [SerializeField] Image fill;
    [SerializeField] Sprite defaultSprite;
    DataManager.Skill target;
    float timer;

    public float Range => target.data.range;
    public bool IsActiveOn => target != null && target.data != null && target.data.id != "" && timer <= 0 && UIManager.Instance.IsAutoSkill;

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
        if (GameManager.Instance.Player.IsDeath || target == null || target.data == null || target.data.id == "" || timer > 0)
        {
            return;
        }

        GameManager.Instance.Player.ActiveSkill(index);
    }

    public void ResetSkill()
    {
        timer = target.data.coolTime;
    }
}
