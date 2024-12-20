using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BuffBase : MonoBehaviour
{
    [SerializeField] GameObject targetParticle;
    [SerializeField] ClientEnum.State state;
    [SerializeField] string id;
    [SerializeField] bool isDebuff;
    [SerializeField] float timer;
    [SerializeField] float value;

    public string ID => id;
    public float Timer => timer;
    public ClientEnum.State State => state;
    public float Value => isDebuff ? -1 * value : value;

    float maxTimer = 0;
    float tick = 0f;
    bool isOn = false;

    void FixedUpdate()
    {
        if (!isOn)
        {
            return;
        }

        timer -= Time.deltaTime;
        tick += Time.deltaTime;

        if (tick > 0.5f )
        {
            tick = 0;
            BuffUpdate();
        }

        if(timer <= 0f )
        {
            isOn = false;
        }
    }

    public virtual void BuffStart(float _timer,float _value)
    {
        isOn = true;
        timer = _timer;
        value = _value;
        maxTimer = timer;
        tick = 0;
    }

    public virtual void BuffUpdate()
    {

    }

    public virtual void BuffEnd()
    {

    }
}
