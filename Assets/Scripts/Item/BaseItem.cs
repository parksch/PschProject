using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseItem 
{
    [SerializeField] string id;
    [SerializeField] ClientEnum.Item type;

    public string ID => id;
    public ClientEnum.Item Type => type;
}
