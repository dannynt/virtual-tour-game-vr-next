using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationFollower : MonoBehaviour
{
    public Transform target;

    private void FixedUpdate()
    {
        this.transform.rotation = target.rotation;
    }
}
