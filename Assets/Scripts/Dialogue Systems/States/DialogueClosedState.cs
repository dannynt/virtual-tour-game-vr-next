using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueClosedState : DialogueState
{
    private DialogueHandler dialogueHandler;

    public DialogueClosedState(DialogueHandler dialogueHandler)
    {
        this.dialogueHandler = dialogueHandler;
    }

    public override void HandleInput(MonoBehaviour player) {}

    public override State Enter()
    {
        dialogueHandler.Hide();
        return this;
    }

    public override State Exit()
    {
        return this;
    }
}
