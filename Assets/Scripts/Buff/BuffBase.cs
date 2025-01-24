using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClientEnum;

public class BuffBase : MonoBehaviour
{
    [SerializeField, ReadOnly] float timer;
    [SerializeField, ReadOnly] float value;
    [SerializeField, ReadOnly] protected BaseCharacter character;
    [SerializeField, ReadOnly] protected ChangeType changeType;
    [SerializeField] protected GameObject targetParticle;
    [SerializeField] protected bool isDebuff;
    [SerializeField] State state;
    [SerializeField] string id;

    public void Stop() => isOn = false;
    public string ID => id;
    public float Timer => timer;
    public State State => state;
    public float Value(State target,ChangeType _changeType)
    {
        if (target == state && changeType == _changeType)
        {
            return isDebuff ? -value : value;
        }
        else
        {
            return 0;
        }
    }

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

    public virtual void BuffCheck(float _timer,float _value,ClientEnum.ChangeType change)
    {
        if (value <= _value && change == changeType)
        {
            isOn = true;
            value = _value;
            timer = _timer;
        }
    }

    public virtual void BuffStart(BaseCharacter baseCharacter,float _timer,float _value,ClientEnum.ChangeType change)
    {
        character = baseCharacter;
        isOn = true;
        value = _value;
        timer = _timer;
        changeType = change;
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
        character = null;
        PoolManager.Instance.Enqueue(id, gameObject);
    }
}
