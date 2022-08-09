using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Hotspot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler 
{
    public abstract void OnVREnter();
    public abstract void OnVRExit();
    public abstract void OnVRDown();
    public abstract void OnVRUp();


    public abstract void OnPointerEnter(PointerEventData eventData);
    public abstract void OnPointerExit(PointerEventData eventData);
    public abstract void OnPointerDown(PointerEventData eventData);
    public abstract void OnPointerUp(PointerEventData eventData);

}
