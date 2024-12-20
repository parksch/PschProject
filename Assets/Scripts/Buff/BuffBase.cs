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

    bool isOn = false;

    void FixedUpdate()
    {
        if (!isOn)
        {
            return;
        }

        timer -= Time.deltaTime;

    }

    public virtual void BuffStart(float _timer,float _value)
    {
        isOn = true;
        timer = _timer;
        value = _value;
    }

    public virtual void BuffUpdate()
    {

    }


    public virtual void BuffEnd()
    {

    }
}
