using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class NPCHotspot : Hotspot
{
    public Dialogue CurrentStartDialogue {get {return this.currentStartDialogue;} set {this.currentStartDialogue = value;}}
    public Dialogue currentStartDialogue;
    [SerializeField] private NPC npc;

    public NPC Npc { get { return this.npc; } }
    
    private bool exited;
 
    public override void OnVREnter() 
    {
        VTGDebug.Log("Enter npc hotspot");
        if (!exited && !DialogueHandler.instance.currentlyContinue) 
        {
            npc.Touch();
            npc.GoInDialogue(true);
            DialogueHandler.instance.StartDialogue(currentStartDialogue, npc.transform);
        }
        exited = false;
    }

    public override void OnVRExit() 
    {
        VTGDebug.Log("Exit npc hotspot");
        exited = true;
        StartCoroutine(Exit());
    }

    private IEnumerator Exit()
    {
        yield return new WaitForSeconds(0.25f);
        if (exited)
        {
            DialogueHandler.instance.EnterState(new DialogueClosedState(DialogueHandler.instance));
            npc.GoInDialogue(false);
            exited = false;
            DialogueHandler.instance.currentlyContinue = false;
        }
    }

    public override void OnVRDown() 
    {
        VTGDebug.Log("Down");
        npc.Touch();
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
