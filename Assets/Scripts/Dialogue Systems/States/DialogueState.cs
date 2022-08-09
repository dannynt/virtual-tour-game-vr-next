using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class DialogueState : State
{
    public override void HandleInput(MonoBehaviour player) {}
    public Action<Dialogue> FinishAction;

    public override State Enter() { return this; }
    public override State Exit() { return this; }
}
