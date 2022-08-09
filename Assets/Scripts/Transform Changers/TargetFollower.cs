using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollower : MonoBehaviour
{
    public Transform target;

    public bool followPosition;
    public bool followRotation;
    public bool rotationX;
    public bool rotationY;
    public bool rotationZ;


    public bool followScale;
    public float followPositionSpeed;
    public float followRotationSpeed;
    public float followScaleSpeed;



    public void FixedUpdate()
    {
        if (target == null) return;

        if (followPosition) transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, transform.position.y, target.position.z), followPositionSpeed * Time.deltaTime);

        var targetRotation = Quaternion.Euler(
            (rotationX) ? target.rotation.eulerAngles.x : transform.rotation.eulerAngles.x, 
            (rotationY) ? target.rotation.eulerAngles.y : transform.rotation.eulerAngles.y, 
            (rotationZ) ? target.rotation.eulerAngles.z : transform.rotation.eulerAngles.z);

        if (followRotation) transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, followRotationSpeed);
        if (followScale) transform.localScale = Vector3.Lerp(transform.localScale, target.localScale, followScaleSpeed * Time.deltaTime);
    }
}
