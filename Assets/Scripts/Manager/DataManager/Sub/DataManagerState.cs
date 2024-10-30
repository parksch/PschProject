using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class DataManager //State
{
    [SerializeField, ReadOnly] PlayerState playerDefaultState;
    public PlayerState PlayerDefaultState => playerDefaultState;

}
