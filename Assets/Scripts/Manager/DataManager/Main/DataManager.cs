using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using JsonClass;

public partial class DataManager : Singleton<DataManager>
{
    [SerializeField] ClientEnum.Language language;
    [SerializeField, ReadOnly] string deviceNum;

    Dictionary<string, int> upgradeLevel = new Dictionary<string, int>();
    public ClientEnum.Language Language { set { language = value; } get { return language; } }
    public void SetDevice(string value) => deviceNum = value;
    public int GetUpgradeLevel(string code) => upgradeLevel[code];

    protected override void Awake()
    {
        base.Awake();
    }

    public void Init()
    {
        List<UpgradeScriptable.UpgradeState> upgradeStates = ScriptableManager.Instance.Get<UpgradeScriptable>(ScriptableType.Upgrade).GetUpgradeType(ClientEnum.UpgradeType.StatePanel);

        for (int i = 0; i < upgradeStates.Count; i++)
        {
            upgradeLevel[upgradeStates[i].name] = 0;
        }

        for (var i = ClientEnum.Draw.Min; i < ClientEnum.Draw.Max; i++)
        {
            Draw shop = ScriptableManager.Instance.Get<DrawScriptable>(ScriptableType.Draw).GetData(i);

            if (shop == null)
            {
                continue;
            }

            for (int j = 0; j < shop.type.shops.Count; j++)
            {
                if (shop.type.shops[j].limit > 0)
                {
                    info.CreateDrawLimit(shop.type.shops[j].nameKey);
                }

                if (shop.type.shops[j].maxLevel > 0)
                {
                    info.CreateDrawCount(shop.type.shops[j].nameKey);
                }
            }
        }
    }
}
