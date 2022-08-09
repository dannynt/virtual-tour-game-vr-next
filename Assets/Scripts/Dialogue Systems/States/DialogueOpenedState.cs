using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueOpenedState : DialogueState
{
    private DialogueHandler dialogueHandler;

    public DialogueOpenedState(DialogueHandler dialogueHandler)
    {
        this.dialogueHandler = dialogueHandler;
    }

    public override void HandleInput(MonoBehaviour player) {}
    public override State Enter()
    {
        dialogueHandler.Show();
        return this;
    }

    public override State Exit()
    {
        return this;
    }
}
