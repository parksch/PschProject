using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffBase : MonoBehaviour
{
    [SerializeField] GameObject targetParticle;
    [SerializeField] ClientEnum.State state;
    [SerializeField] string id;
    [SerializeField] bool isDebuff;
    [SerializeField] float timer;
    [SerializeField] float value;

    public void Stop() => isOn = false;
    public string ID => id;
    public float Timer => timer;
    public ClientEnum.State State => state;
    public float Value(ClientEnum.State target)
    {
        if (target == state)
        {
            return isDebuff ? -value : value;
        }
        else
        {
            return 0;
        }
    }

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
        timer = Mathf.Round(timer * 100f)/100f;

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

    public virtual void BuffCheck(float _timer,float _value)
    {
        if (value <= _value)
        {
            isOn = true;
            value = _value;
            timer = _timer;
            maxTimer = timer;
        }
    }

    public virtual void BuffStart(float _timer,float _value)
    {
        isOn = true;
        value = _value;
        timer = _timer;
        maxTimer = timer;
        tick = 0;
        gameObject.SetActive(true);
    }

    public virtual void BuffUpdate()
    {

    }

    public virtual void BuffEnd()
    {
    }

    public void Enqueue()
    {
        PoolManager.Instance.Enqueue(id, gameObject);
    }
}
