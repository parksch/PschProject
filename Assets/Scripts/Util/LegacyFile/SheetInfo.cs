#if UNITY_EDITOR
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class Req_ServerSheet
{
    public List<SheetInfo> SheetsInfo { get; set; } = new List<SheetInfo>();
}

public class SheetInfo
{
    public int gid { get; set; }
    public string sheet_name { get; set; }
}

public class ServerDataResetMgr : Singleton<ServerDataResetMgr>
{
    public async UniTask<string> UserDataChange(int userIndex, byte eventId, int value)
    {
        return null;
    }

    public async UniTask<string> ServerDataUpdate(Req_ServerSheet sheetInfo)
    {
        return null;
    }
}

public class ExternalDataAsset
{
    //== Only parent
}
#endif