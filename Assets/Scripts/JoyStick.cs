using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler //, IPointerClickHandler
{
    public Transform moveTrans;

    private float inputXPos;
    private Vector3 rotateValue = new Vector3(0f, 10f, 0f);

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.position.x > inputXPos)
        {
            moveTrans.localEulerAngles -= rotateValue;
        }
        else
            moveTrans.localEulerAngles += rotateValue;

        inputXPos = eventData.position.x;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        inputXPos = eventData.position.x;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputXPos = 0;
    }
}

