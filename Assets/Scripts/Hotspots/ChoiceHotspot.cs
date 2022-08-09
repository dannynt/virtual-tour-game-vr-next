using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class ChoiceHotspot : Hotspot
{
    public DialogueChoice choice {get; set;}
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image border;

    public void Initialize(DialogueChoice choice)
    {
        this.choice = choice;
        this.text.text = Memory.instance.FilterConstraints(choice.text);
        border.fillAmount = 0f;
        transform.localScale = Vector3.zero;
        Show();
    }

    private void Hide()
    {
        transform.DOScale(Vector3.zero, 0.15f).SetEase(Ease.InOutQuad);
    }

    private void Show()
    {
        transform.DOScale(Vector3.one, 0.15f).SetEase(Ease.InOutQuad).OnComplete(() =>
        {
            DialogueHandler.instance.currentlyContinue = false;
        });
    }

    public override void OnVREnter() 
    {
        VTGDebug.Log("Enter choice hotspot");
        if (border == null) return;
        var fillAmount = border.fillAmount;
        transform.DOScale(Vector3.one * 1.1f, 0.15f).SetEase(Ease.InOutQuad);

        var seq = DOTween.Sequence();
        seq.Append(DOTween.To(() => fillAmount, x => fillAmount = x, 1f, 0.2f).SetEase(Ease.InOutQuad).OnUpdate(() =>
        {
            if (border != null)
            {
                border.fillAmount = fillAmount;
            }
        }));
    }

    public override void OnVRExit() 
    {
        VTGDebug.Log("Exit choice hotspot");
        if (border == null) return;
        var fillAmount = border.fillAmount;
        transform.DOScale(Vector3.one * 1f, 0.15f).SetEase(Ease.InOutQuad);

        var seq = DOTween.Sequence();
        seq.Append(DOTween.To(() => fillAmount, x => fillAmount = x, 0f, 0.2f).SetEase(Ease.InOutQuad).OnUpdate(() =>
        {
            if (border != null)
            {
                border.fillAmount = fillAmount;
            }
        }));
    }

    public override void OnVRDown() 
    {
        VTGDebug.Log("Click choice hotspot");
        DialogueHandler.instance.ContinueDialogue(this.choice.nextDialogue);
        Memory.instance.AddConstraintsInMemory(choice.text);
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
}
