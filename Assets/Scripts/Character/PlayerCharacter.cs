using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : BaseCharacter
{
    [SerializeField] PlayerState State => state as PlayerState;
}
