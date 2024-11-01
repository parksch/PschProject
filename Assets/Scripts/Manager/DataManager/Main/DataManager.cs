using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using JsonClass;

public partial class DataManager : Singleton<DataManager>
{
    [SerializeField] ClientEnum.Language language;
    [SerializeField, ReadOnly] string deviceNum;

    public ClientEnum.Language Language { set { language = value; } get { return language; } }
    public void SetDevice(string value) => deviceNum = value;

    protected override void Awake()
    {
        base.Awake();
    }

    public void Init()
    {
        StateInit();
        InfoInit();
    }
}
