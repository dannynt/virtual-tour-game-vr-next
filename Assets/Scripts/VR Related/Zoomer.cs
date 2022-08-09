using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using TMPro;

public class Zoomer : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private TextMeshProUGUI debugText;

    public Vector3 ZeroPosition { get; set; }

    private void Start()
    {
        var tourer = FindObjectOfType<Tourer>();

        foreach (var room in GameObject.FindObjectsOfType<Room>())
        {
            if (room.title == "Cafe") tourer.TourerRoom = room;
        }

        new TeleportCommand(VRInputType.TELEPORT).Execute(tourer);

        //ZeroPosition = tourer.TourerRoom.TeleportPoint.transform.position;
        ZeroPosition = target.transform.position;
    }

    private void FixedUpdate() 
    {
        Vector3 targetPos = new Vector3(target.transform.position.x, ZeroPosition.y, target.transform.position.z);
        float distanceFromInitialToTargetPosition = Vector3.Distance(ZeroPosition, targetPos);
        XRDevice.fovZoomFactor = 0.9f + distanceFromInitialToTargetPosition / 3f;

        if (debugText != null) debugText.text = distanceFromInitialToTargetPosition + " Us";
    }

    public void ZeroPositionOut()
    {
        ZeroPosition = target.transform.position;
    }
}
