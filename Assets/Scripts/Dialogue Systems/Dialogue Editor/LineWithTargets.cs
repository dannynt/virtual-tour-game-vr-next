using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LineWithTargets : MonoBehaviour
{
    public LineRenderer line;
    public Transform start;
    public Transform end;

    public Transform label;

    public Transform icon;

    public void Init(LineRenderer line, Transform start, Transform end, Transform label, Transform icon)
    {
        this.line = line;
        this.start = start;
        this.end = end;
        this.label = label;
        this.icon = icon;
    }
}
