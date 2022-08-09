using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNullState : DialogueState
{
    public override void HandleInput(MonoBehaviour player) {}

    public override State Enter()
    {
        return this;
    }

    public override State Exit()
    {
        return this;
    }
}
