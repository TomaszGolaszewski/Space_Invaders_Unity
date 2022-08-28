using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RightButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static bool rightPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        rightPressed = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        rightPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        rightPressed = false;
    }
}
