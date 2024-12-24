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
    public bool IsActiveOn => IsNull && timer <= 0 && UIManager.Instance.IsAutoSkill;
    public bool IsNull => target == null || target.data == null || target.data.id == "";

    public void SetSkill(DataManager.Skill skill)
    {
        target = skill;

        if (IsNull)
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
        if (IsNull)
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
        if (GameManager.Instance.Player.IsDeath || IsNull || timer > 0)
        {
            return;
        }

        GameManager.Instance.Player.ActiveSkill(index);
    }

    public void ResetSkill()
    {
        if (IsNull)
        {
            return;
        }

        timer = target.data.coolTime;
    }
}
