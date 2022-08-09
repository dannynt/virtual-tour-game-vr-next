using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class State
{
    public abstract void HandleInput(MonoBehaviour player);

    public abstract State Enter();

    public abstract State Exit();
}