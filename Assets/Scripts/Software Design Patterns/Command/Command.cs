using System;
using System.Reflection;
using UnityEngine;

public abstract class Command
{
    public abstract VRInputType VRInput { get; set; }
    public abstract void Execute(MonoBehaviour receiver);
}
