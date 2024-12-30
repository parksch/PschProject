using JsonClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonPanel : BasePanel
{
    [SerializeField,ReadOnly] UIButton sweep;
    [SerializeField,ReadOnly] UIButton challenge;
    [SerializeField,ReadOnly] Text title;
    [SerializeField,ReadOnly] RectTransform content;
    [SerializeField,ReadOnly] UIDungeonSlot prefab;
    [SerializeField,ReadOnly] List<UIDungeonSlot> slots = new List<UIDungeonSlot>();
    [SerializeField] UIRewardSlot rewardSlot;
    [SerializeField] List<UIRewardSlot> rewardSlots;

    DungeonsData current;

    public override void FirstLoad()
    {
        DungeonsDataScriptable scriptable = ScriptableManager.Instance.Get<DungeonsDataScriptable>(ScriptableType.DungeonsData);

        for (int i = 0; i < scriptable.dungeonsData.Count; i++)
        {
            if (i == 0)
            {
                prefab.SetData(scriptable.dungeonsData[i]);
                slots.Add(prefab);
            }
            else
            {
                UIDungeonSlot slot = Instantiate(prefab,content).GetComponent<UIDungeonSlot>();
                slot.SetData(scriptable.dungeonsData[i]);
                slots.Add(slot);
            }
        }
    }

    public override void OnUpdate()
    {
    }

    public override void Close()
    {

    }

    public override void Open()
    {
        base.Open();
        SetSlot(slots[0]);
    }

    public void OnClickDungeonSlot(UIDungeonSlot slot)
    {
        SetSlot(slot);
    }

    void SetSlot(UIDungeonSlot slot)
    {
        current = slot.Target;
        title.text = current.Title();

        switch (current.NeedGoods())
        {
            case ClientEnum.Goods.GoldDungeonTicket:
                sweep.SetInterractable(DataManager.Instance.GetInfo.CurrentGoldDungeon > 0 && DataManager.Instance.CheckGoods(ClientEnum.Goods.GoldDungeonTicket, 1));
                challenge.SetInterractable(DataManager.Instance.GetInfo.CurrentGoldDungeon < current.maxLevel && DataManager.Instance.CheckGoods(ClientEnum.Goods.GoldDungeonTicket, 1));
                break;
            case ClientEnum.Goods.GemDungeonTicket:
                sweep.SetInterractable(DataManager.Instance.GetInfo.CurrentGemDungeon > 0 && DataManager.Instance.CheckGoods(ClientEnum.Goods.GemDungeonTicket, 1));
                challenge.SetInterractable(DataManager.Instance.GetInfo.CurrentGemDungeon < current.maxLevel && DataManager.Instance.CheckGoods(ClientEnum.Goods.GemDungeonTicket, 1));
                break;
            default:
                sweep.SetInterractable(false);
                challenge.SetInterractable(false);
                break;
        }
    }
}
