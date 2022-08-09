using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using TMPro;

public class MoveHotspot : Hotspot
{
    public string RoomToMove {get {return this.roomToMove;} set {this.roomToMove = value;}}
    public string roomToMove;
    private Vector3 oldSizeOfGraphic;

    private Tourer tourer;

    private bool hide;


    private void Start() 
    {
        transform.localScale = transform.localScale * 0.75f;
        oldSizeOfGraphic = transform.localScale * 0.75f;
        tourer = GameObject.FindObjectOfType<Tourer>();
        var xrInteraction = GetComponent<XRSimpleInteractable>();
        xrInteraction.colliders.Clear();
        xrInteraction.colliders.Add(GetComponent<Collider>());
        HideBubble();
        GetComponentInChildren<TextMeshProUGUI>().text = roomToMove;
    }

    private void ShakeBubble()
    {
        hide = false;
        var bubble = GetComponentInChildren<Image>().transform;
        var seq = DOTween.Sequence();
        seq.Append(bubble.DOScale(Vector3.one, 0.1f).SetEase(Ease.InOutQuad));
        seq.Append(bubble.DOShakeScale(0.5f, 0.2f, 10, 0).SetEase(Ease.InOutQuad));
        if (!hide) seq.Append(bubble.DOScale(Vector3.one, 0.01f).SetEase(Ease.InOutQuad));
    }

    private void HideBubble()
    {
        var bubble = GetComponentInChildren<Image>().transform;
        var seq = DOTween.Sequence();
        seq.Append(bubble.DOScale(Vector3.zero, 0.1f).SetEase(Ease.InOutQuad));
        hide = true;
    }

    public override void OnVREnter() 
    {
        AnimateEnter();
        ShakeBubble();
    }

    public override void OnVRExit() 
    {
        AnimateExit();
        HideBubble();
    }

    public override void OnVRDown() 
    {
        AnimateDown();
        FindObjectOfType<Tourer>().Teleport(roomToMove);
    }

    public override void OnVRUp() {}

    public override void OnPointerDown(PointerEventData eventData) 
    {
        OnVRDown();
    }

    public override void OnPointerEnter(PointerEventData eventData) 
    {
        OnVREnter();
    }

    public override void OnPointerExit(PointerEventData eventData) 
    {
        OnVRExit();
    }

    public override void OnPointerUp(PointerEventData eventData) 
    {
        OnVRUp();
    }

    private void AnimateExit()
    {
        var seq = DOTween.Sequence();
        //seq.Append(transform.DOShakeScale(0.4f, 0.25f, 10, 1, true).SetEase(Ease.InOutQuad));
        seq.Append(transform.DOScale(oldSizeOfGraphic, 0.25f).SetEase(Ease.InOutQuad));
    }

    private void AnimateDown()
    {
        var seq = DOTween.Sequence();
        seq.Append(transform.DOShakeScale(0.4f, 0.25f, 10, 1, true).SetEase(Ease.InOutQuad));
        seq.Append(transform.DOScale(oldSizeOfGraphic, 0.25f).SetEase(Ease.InOutQuad));
    }

    private void AnimateEnter()
    {
        var seq = DOTween.Sequence();
        //seq.Append(transform.DOShakeScale(0.4f, 0.25f, 10, 1, true).SetEase(Ease.InOutQuad));
        seq.Append(transform.DOScale(oldSizeOfGraphic * 1.5f, 0.25f).SetEase(Ease.InOutQuad));
    }
    
    private void Update() 
    {
        if (tourer != null) transform.LookAt(tourer.transform.position, Vector3.up);
    }
}
