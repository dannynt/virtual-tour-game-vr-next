using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Dialogue : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private string id;
    [SerializeField] private bool checkpoint;
    public string text { get { return this.inputField.text; } }
    [SerializeField] private List<DialogueChoice> choices;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private string objective;

    public string Id {get {return this.id;} set {this.id = value;}}
    public bool Checkpoint {get {return this.checkpoint;} set {this.checkpoint = value;}}
    public List<DialogueChoice> Choices {get {return this.choices;} set {this.choices = value;}}
    public string Objective {get {return this.objective;} set {this.objective = value;}}




    private void Start()
    {
        this.inputField = GetComponentInChildren<TMP_InputField>();
        var thisPos = transform.position;
        this.id = "d:" + (int) thisPos.x + ":" + (int) thisPos.y + ":" + (int) thisPos.z + ":" + Random.Range(-1000, 1000);
        Deselect();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(Vector3.one * 1.1f, 0.2f).SetEase(Ease.InOutQuad);
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (Keyboard.current.deleteKey.isPressed)
        {
            var allDialogues = FindObjectsOfType<Dialogue>();
            foreach (var dialogue in allDialogues)
            {
                for (int i = 0; i < dialogue.choices.Count; i++)
                {
                    var choice = dialogue.choices[i];
                    
                    if (choice == null) continue;

                    if (choice.nextDialogue == this)
                    {
                        dialogue.choices.Remove(choice);
                        Destroy(choice.gameObject);
                        continue;
                    }
                }
            }

            var seq = DOTween.Sequence();
            seq.Append(transform.DOScale(Vector3.zero, 0.1f).SetEase(Ease.InOutQuad)).OnComplete(() => Destroy(this.gameObject));

            return;
        }
        DialogueEditor.instance.AddDialogue(this);
        DialogueEditor.instance.activeDialogue = this;
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        DialogueEditor.instance.activeDialogue = null;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.InOutQuad);
    }

    public void Select()
    {
        GetComponentInChildren<SpriteRenderer>().color = Color.yellow;
    }

    public void Deselect()
    {
        var newColor = (checkpoint) ? Color.green : Color.white;
        GetComponentInChildren<SpriteRenderer>().color = newColor;
    }

}