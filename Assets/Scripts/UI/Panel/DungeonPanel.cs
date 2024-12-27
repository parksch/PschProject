using JsonClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonPanel : BasePanel
{
    [SerializeField,ReadOnly] RectTransform content;
    [SerializeField,ReadOnly] UIDungeonSlot prefab;
    [SerializeField,ReadOnly] List<UIDungeonSlot> slots = new List<UIDungeonSlot>();

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
    }
}
