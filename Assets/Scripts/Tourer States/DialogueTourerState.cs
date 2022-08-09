using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTourerState : State
{
    public DialogueTourerState() {}

    public override void HandleInput(MonoBehaviour player)
    {
        if ((player as Tourer).Input.Count == 0) return;

        VRInputType input = (player as Tourer).Input.Dequeue();

        foreach (Command c in InputHandler.Commands.Values)
        {
            if (c.VRInput == input)
            {
                c.Execute(player);
            }
        }
    }

    public override State Enter()
    {
        InputHandler.ClearCommands();
        return this;
    }

    public override State Exit()
    {
        return this;
    }
    
}
