using JsonClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBuffSlots : MonoBehaviour
{
    [SerializeField, ReadOnly] RectTransform content;
    [SerializeField, ReadOnly] UIBuffSlot slotPrefab;
    [SerializeField, ReadOnly] List<UIBuffSlot> slots;

    public void AddBuff(BuffBase buff,float value,float timer)
    {
        bool isSet = false;
        content.anchoredPosition = Vector2.zero;

        UIBuffSlot uIBuffSlot = slots.Find(x => x.ID == buff.ID && x.gameObject.activeSelf);

        if (uIBuffSlot != null)
        {
            uIBuffSlot.SetValue(value, timer);
            return;
        }

        for (int i = 0; i < slots.Count; i++)
        {
            if (!slots[i].gameObject.activeSelf)
            {
                slots[i].SetSlot(buff,value, timer);
                isSet = true;
            }
        }

        if (!isSet)
        {
            UIBuffSlot slot = Instantiate(slotPrefab,content).GetComponent<UIBuffSlot>();
            slot.SetSlot(buff,value, timer);
            slots.Add(slot);
        }
    }

    public void ResetBuffs()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].End();
        }
    }

    public void CheckBuff()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].UpdateTimer();
        }
    }
}
