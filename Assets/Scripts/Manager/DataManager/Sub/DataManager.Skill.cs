using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class DataManager 
{
    [SerializeField, ReadOnly] List<Skill> skills = new List<Skill>();

    [System.Serializable]
    public class Skill
    {

    }
}
