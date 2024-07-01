using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAnimationEvent : MonoBehaviour
{
    System.Action start;
    System.Action mid;
    System.Action end;

    public void SetStart(System.Action start)
    { 
        this.start = start; 
    }

    public void SetMid(System.Action mid) 
    {  
        this.mid = mid;
    }

    public void SetEnd(System.Action end)
    { 
        this.end = end;
    }

    public void ResetEvent()
    {
        start = null;
        mid = null;
        end = null;
    }

    public void ActionStart()
    {
        start?.Invoke();
    }

    public void ActionMid()
    {
        mid?.Invoke();
    }

    public void ActionEnd() 
    { 
        end?.Invoke();
    }
}
