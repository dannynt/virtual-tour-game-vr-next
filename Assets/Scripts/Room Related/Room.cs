using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public string title;

    public TeleportationPoint TeleportPoint {get { return GetComponentInChildren<TeleportationPoint>(); } }
}
