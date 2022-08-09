using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class DialogueHandler : Subject
{
    private Dialogue currentDialogue;
    private List<Dialogue> allDialogues;

    private static DialogueHandler _instance;

    [SerializeField] private TextMeshProUGUI mainText;

    [SerializeField] private Transform choicesGrid;
    [SerializeField] private GameObject choicePrefab;
    [SerializeField] private Transform bubble;

    private Transform currentTarget;

    private List<GameObject> choicesPrefabs;

    public bool currentlyContinue { get; set; }
 
    public static DialogueHandler instance 
    {
        get
        {
            if (!_instance) _instance = FindObjectOfType<DialogueHandler>();

            if (!_instance) _instance = new GameObject("Dialogue handler").AddComponent<DialogueHandler>();

            return _instance;
        }
    }

    protected override void Start()
    {
        choicesPrefabs = new List<GameObject>();
        base.Start();
        EnterState(new DialogueClosedState(this));
    }

    private Dialogue FindDialogue(string id)
    {
        foreach (var dialogue in allDialogues)
        {
            if (dialogue.Id == id)
            {
                return dialogue;
            }
        }

        return null;
    }

    private void ResetLocation(Transform newParent)
    {
        transform.parent = newParent;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }

    public void Hide()
    {
        transform.DOScale(Vector3.zero, 0.15f).SetEase(Ease.InOutQuad);
    }

    public void Show()
    {
        transform.DOScale(Vector3.one, 0.15f).SetEase(Ease.InOutQuad);
    }

    public void ChangeMainText(string newText)
    {
        //mainText.text = newText;
        mainText.text = "";
        StartCoroutine(AppendText(newText));
    }

    private void ShakeBubble()
    {
        var seq = DOTween.Sequence();
        seq.Append(bubble.DOShakeScale(0.5f, 0.2f, 10, 0).SetEase(Ease.InOutQuad));
        seq.Append(bubble.DOScale(Vector3.one, 0.2f).SetEase(Ease.InOutQuad));
    }

    private IEnumerator AppendText(string text)
    {
        for (int i = 0; i < text.Length; i++)
        {
            yield return new WaitForSeconds(0.01f);
            mainText.text += text.ToCharArray()[i];
        }
    }

    private void LoadChoices(List<DialogueChoice> choices)
    {
        foreach (var choice in choices)
        {
            if (!Memory.instance.AreConstraintsInMemory(choice.text)) continue;
            if (Memory.instance.AreConstraintsAdded(choice.text)) continue;
            var choiceGO = Instantiate(choicePrefab, Vector3.zero, Quaternion.identity, choicesGrid);
            var choiceHotspot = choiceGO.GetComponent<ChoiceHotspot>();
            choiceHotspot.Initialize(choice);
            choicesPrefabs.Add(choiceGO);
            choiceGO.transform.localPosition = Vector3.zero;
            choiceGO.transform.localRotation = Quaternion.identity;
        }
    }

    public void ClearChoices()
    {
        for (int i = 0; i < choicesPrefabs.Count; i++)
        {
            Destroy(choicesPrefabs[i].gameObject);
        }

        choicesPrefabs.Clear();
    }

    public void StartDialogue(Dialogue dialogueToStart, Transform target)
    {
        if (dialogueToStart == null)
        {
            EnterState(new DialogueClosedState(this));
            return;
        }

        if (target) currentTarget = target;
        
        ShakeBubble();
        ResetLocation(target);
        ChangeMainText(dialogueToStart.text);
        ClearChoices();
        LoadChoices(dialogueToStart.Choices);
        currentDialogue = dialogueToStart;
        EnterState(new DialogueOpenedState(this));
    }

    public void ContinueDialogue(Dialogue dialogueToContinue)
    {
        if (dialogueToContinue == null)
        {
            EnterState(new DialogueClosedState(this));
            return;
        }

        currentlyContinue = true;

        ShakeBubble();
        ChangeMainText(dialogueToContinue.text);
        ClearChoices();
        LoadChoices(dialogueToContinue.Choices);
        currentDialogue = dialogueToContinue;
        
        var npc = currentTarget.parent.GetComponentInChildren<NPCHotspot>();

        if (npc)
        {
            npc.Npc.Approve();
        }

        // Save dialogue checkpoint.
        if (currentDialogue.Checkpoint)
        {
            if (npc)
            {
                npc.currentStartDialogue = currentDialogue;
            }
        }

        if (currentDialogue.Objective != null)
        {
            if (currentDialogue.Objective != "")
            {
                var objective = GameObject.FindGameObjectWithTag("Objective");
                if (objective)
                {
                    var text = objective.GetComponent<TextMeshPro>();
                    text.text = currentDialogue.Objective;
                }
            }
        }
    }

    private void Update()
    {
        this.state.HandleInput(this);
    }
}
