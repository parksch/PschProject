using ClientEnum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOption 
{
    public Grade Grade => grade;
    public ChangeType ChangeType => changeType;
    public float OptionSet => value;

    Grade grade;
    ChangeType changeType;
    float value;

    public void SetValue(ChangeType _changeType,float _value, Grade _grade = Grade.Common)
    {
        changeType = _changeType;
        value = _value;
        grade = _grade;
    }

    public void Reset()
    {
        changeType = ChangeType.Sum;
        value = 0;
        grade = Grade.Common;
    }
}
