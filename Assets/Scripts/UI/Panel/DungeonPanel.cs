using JsonClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonPanel : BasePanel
{
    [SerializeField,ReadOnly] DungeonSweepPanel sweepPanel;
    [SerializeField,ReadOnly] UIButton sweep;
    [SerializeField,ReadOnly] UIButton challenge;
    [SerializeField,ReadOnly] Text title;
    [SerializeField,ReadOnly] RectTransform content;
    [SerializeField,ReadOnly] RectTransform rewardContent;
    [SerializeField,ReadOnly] UIDungeonSlot prefab;
    [SerializeField,ReadOnly] List<UIDungeonSlot> slots = new List<UIDungeonSlot>();
    [SerializeField,ReadOnly] UIRewardSlot rewardSlot;
    [SerializeField,ReadOnly] List<UIRewardSlot> rewardSlots;

    DungeonsData current;

    public override void FirstLoad()
    {
        sweepPanel.CopyTopMenu(ActiveTop);

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
        int level = 0;

        rewardContent.anchoredPosition = Vector2.zero;
        current = slot.Target;
        title.text = current.Title();

        switch (current.NeedGoods())
        {
            case ClientEnum.Goods.GoldDungeonTicket:
                level = DataManager.Instance.CurrentGoldDungeon;
                sweep.SetInterractable(level > 0 && DataManager.Instance.CheckGoods(ClientEnum.Goods.GoldDungeonTicket, 1));
                challenge.SetInterractable(level < current.maxLevel && DataManager.Instance.CheckGoods(ClientEnum.Goods.GoldDungeonTicket, 1));
                break;
            case ClientEnum.Goods.GemDungeonTicket:
                level = DataManager.Instance.CurrentGemDungeon;
                sweep.SetInterractable(level > 0 && DataManager.Instance.CheckGoods(ClientEnum.Goods.GemDungeonTicket, 1));
                challenge.SetInterractable(level < current.maxLevel && DataManager.Instance.CheckGoods(ClientEnum.Goods.GemDungeonTicket, 1));
                break;
            default:
                sweep.SetInterractable(false);
                challenge.SetInterractable(false);
                break;
        }

        for (int i = 0; i < rewardSlots.Count; i++)
        {
            rewardSlots[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < current.dungeonReward.Count; i++)
        {
            if ( rewardSlots.Count > i)
            {
                rewardSlots[i].SetTypeItem(current.RewardType(), current.dungeonReward[i].index, current.dungeonReward[i].Value(level));
            }
            else
            {
                UIRewardSlot prefab = Instantiate(rewardSlot.gameObject, rewardContent).GetComponent<UIRewardSlot>();
                prefab.SetTypeItem(current.RewardType(), current.dungeonReward[i].index, current.dungeonReward[i].Value(level));
                rewardSlots.Add(prefab);
            }

            rewardSlots[i].gameObject.SetActive(true);
        }
    }

    public void OpenSweep()
    {
        sweepPanel.SetTarget(current);
        UIManager.Instance.AddPanel(sweepPanel);
    }

    public void OnClickChallenge()
    {
        GameManager.Instance.OnChangeGameMode(current.GameMode());
        UIManager.Instance.BackPanel();
    }
}
