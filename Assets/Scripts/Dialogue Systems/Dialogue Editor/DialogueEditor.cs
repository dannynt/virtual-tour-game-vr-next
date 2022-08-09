using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;
using System.IO;
using DG.Tweening;

public class DialogueEditor : MonoBehaviour
{
    public bool active;

    [SerializeField] private Material lineMaterial;

    [SerializeField] private DialogueChoice dialogueChoice;

    [SerializeField] private GameObject dialoguePrefab;

    private static DialogueEditor _instance;


    public static DialogueEditor instance 
    {
        get
        {
            if (!_instance) _instance = FindObjectOfType<DialogueEditor>();

            if (!_instance) _instance = new GameObject("Dialogue editor").AddComponent<DialogueEditor>();

            return _instance;
        }
    }

    public Dialogue activeDialogue;

    public Dialogue startDialogue;

    public Dialogue endDialogue;

    [SerializeField] private List<LineWithTargets> lineWithTargets;

    private float cameraOrthoSizeTarget;

    private Vector3 cameraPosition;

    private void Start()
    {
        if (!active) return;
        //if(lineWithTargets == null) lineWithTargets = new List<LineWithTargets>();
        cameraOrthoSizeTarget = Camera.main.orthographicSize;
        cameraPosition = Camera.main.transform.position;
    }

    public void AddDialogue(Dialogue d)
    {
        if (!active) return;
        if (startDialogue == null)
        {
            startDialogue = d;
            startDialogue.Select();
            Debug.Log("Choose now ending dialogue");
        }
        else if (endDialogue == null)
        {
            if (!Keyboard.current.ctrlKey.isPressed) return;
            endDialogue = d;
            if (startDialogue != endDialogue) CreateChoiceBetweenDialogues(startDialogue, endDialogue);
            //startDialogue.Deselect();
            //startDialogue = null;
            endDialogue = null;
        }
    }

    public void CreateChoiceBetweenDialogues(Dialogue a, Dialogue b)
    {
        if (a == b)
        {
            b = null;
            return;
        }

        // Visualize choice by line.
        var line = new GameObject("Line [" + a.gameObject.name + "->" + b.gameObject.name + "]" + lineWithTargets.Count);
        line.transform.parent = transform;
        var lineComp = line.AddComponent<LineRenderer>();
        lineComp.startWidth = 0.25f;
        lineComp.endWidth = 0.25f;
        lineComp.positionCount = 2;
        lineComp.SetPosition(0, startDialogue.transform.position);
        // lineComp.SetPosition(1, endDialogue.transform.position);
        lineComp.material = lineMaterial;

        // Create actual choice.
        var choice = GameObject.Instantiate(dialogueChoice.gameObject, PositionBetweenAB(a.transform.position, b.transform.position, 0.5f), Quaternion.identity, a.transform);
        var choiceComp = choice.GetComponent<DialogueChoice>();
        choiceComp.nextDialogue = b;
        a.Choices.Add(choiceComp);

        Debug.Log("Choice added");

        var lwt = line.AddComponent<LineWithTargets>();
        lwt.Init(lineComp, a.transform, b.transform, choice.transform, choice.GetComponentInChildren<SpriteRenderer>().transform);

        lineWithTargets.Add(lwt);
    }

    private Vector3 PositionBetweenAB(Vector3 a, Vector3 b, float distance)
    {
        return Vector3.Lerp(a, b, distance);
    }

    private void Update()
    {
        if (!active) return;
        // Camera scrolling        
        var scroll = Mouse.current.scroll.ReadValue();

        if (scroll.y > 0f) cameraOrthoSizeTarget -= 2f;
        else if (scroll.y < 0f) cameraOrthoSizeTarget += 2f;

        // Camera position changing
        if (Mouse.current.middleButton.isPressed)
        {
            var worldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            cameraPosition = new Vector3(worldPos.x, worldPos.y, cameraPosition.z);
        }

        // Deselect dialogues
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (startDialogue) startDialogue.Deselect();
            if (endDialogue) endDialogue.Deselect();
            startDialogue = null;
            endDialogue = null;
        }

        // Create new dialogue
        if (Keyboard.current.dKey.wasPressedThisFrame && Keyboard.current.ctrlKey.isPressed)
        {
            var worldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            var newDialogue = GameObject.Instantiate(dialoguePrefab, new Vector3(worldPos.x, worldPos.y, 0f), Quaternion.identity, transform);
            newDialogue.transform.localScale = Vector3.zero;
            var seq = DOTween.Sequence();
            seq.Append(newDialogue.transform.DOScale(Vector3.one, 0.02f).SetEase(Ease.InOutQuad));

            if (Keyboard.current.fKey.isPressed)
            {
                var newDialogueComp = newDialogue.GetComponentInChildren<Dialogue>();
                newDialogueComp.Checkpoint = true;
                newDialogueComp.Deselect();
            }
        }

        // Save dialogues
        /*if (Keyboard.current.sKey.wasPressedThisFrame && Keyboard.current.ctrlKey.isPressed)
        {
            gameObject.name = "Dialogues";
            var success = false;

            if (!Directory.Exists("Assets/Prefabs")) AssetDatabase.CreateFolder("Assets", "Prefabs");
            string localPath = "Assets/Prefabs/" + gameObject.name + ".prefab";

            // Make sure the file name is unique, in case an existing Prefab has the same name.
            localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

            PrefabUtility.SaveAsPrefabAsset(gameObject, localPath, out success);
            Debug.Log("Saved " + success);
        }*/
    }

    private void FixedUpdate()
    {
        
        for (int i = 0; i < lineWithTargets.Count; i++)
        {
            var lwt = lineWithTargets[i];

            if (lwt.start == null || lwt.end == null || lwt.label == null)
            {
                Destroy(lwt.line.gameObject);
                lineWithTargets.Remove(lwt);
                continue;
            }

            lwt.line.SetPosition(0, Vector3.Lerp(lwt.line.GetPosition(0), lwt.start.position, 16f * Time.deltaTime));
            lwt.line.SetPosition(1, Vector3.Lerp(lwt.line.GetPosition(1), lwt.end.position, 16f * Time.deltaTime));
            lwt.label.position = Vector3.Lerp(lwt.label.position, PositionBetweenAB(lwt.start.position, lwt.end.position, 0.5f), 16f * Time.deltaTime);
            lwt.icon.position = Vector3.Lerp(lwt.label.position, PositionBetweenAB(lwt.start.position, lwt.end.position, 0.5f), 16f * Time.deltaTime);
            lwt.icon.rotation = Quaternion.LookRotation(lwt.end.position - lwt.icon.transform.position, Vector3.up);
        }
        if (!active) return;

        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, cameraOrthoSizeTarget, 5f * Time.deltaTime);

        var worldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        if (activeDialogue != null) activeDialogue.transform.position = new Vector3(worldPos.x, worldPos.y, activeDialogue.transform.position.z);

        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, cameraPosition, 2.5f * Time.deltaTime);
    }
}
