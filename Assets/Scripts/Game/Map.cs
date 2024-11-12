using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] Spawn spawn; 

    public void CreateEnemy(List<string> monsters)
    {
        spawn.CreateEnemy(monsters);
    }
}
