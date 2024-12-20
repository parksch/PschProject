using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BuffBase : MonoBehaviour
{
    [SerializeField] float timer;
    [SerializeField] ClientEnum.State state;
    [SerializeField] float value;

    public float Timer => timer;
    public ClientEnum.State State => state;
    public float Value => value;

    void FixedUpdate()
    {
        
    }

    public virtual void BuffStart()
    {

    }

    public virtual void BuffEnd()
    {

    }
}
