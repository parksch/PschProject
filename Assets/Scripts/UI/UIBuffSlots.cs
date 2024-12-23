using JsonClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBuffSlots : MonoBehaviour
{
    [SerializeField, ReadOnly] RectTransform content;
    [SerializeField, ReadOnly] UIBuffSlot slotPrefab;
    [SerializeField, ReadOnly] List<UIBuffSlot> slots;

    public void SetBuff(BuffData buffData,float value,float timer)
    {
        bool isSet = false;
        content.anchoredPosition = Vector2.zero;

        UIBuffSlot uIBuffSlot = slots.Find(x => x.ID == buffData.name && x.gameObject.activeSelf);

        if (uIBuffSlot != null)
        {
            uIBuffSlot.SetValue(value, timer);
            return;
        }

        for (int i = 0; i < slots.Count; i++)
        {
            if (!slots[i].gameObject.activeSelf)
            {
                slots[i].SetSlot(buffData,value, timer);
                isSet = true;
            }
        }

        if (!isSet)
        {
            UIBuffSlot slot = Instantiate(slotPrefab,content).GetComponent<UIBuffSlot>();
            slot.SetSlot(buffData,value, timer);
            slots.Add(slot);
        }
    }
}
