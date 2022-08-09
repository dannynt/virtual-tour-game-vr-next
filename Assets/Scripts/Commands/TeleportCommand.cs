using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TeleportCommand : Command
{
    public override VRInputType VRInput { get; set; }

    public TeleportCommand(VRInputType VRInput)
    {
        this.VRInput = VRInput;
    }

    public override void Execute(MonoBehaviour receiver)
    {
        if (!(receiver is Tourer))
        {
            Debug.LogWarning("Trying to teleport non-tourer.");
            return;
        }
        /*receiver.transform.DOMove((receiver as Tourer).TourerRoom.TeleportPoint.transform.position, 0.5f, true).SetEase(Ease.InOutQuad).OnComplete(() => 
        {
            receiver.transform.position = (receiver as Tourer).TourerRoom.TeleportPoint.transform.position;
            GameObject.FindObjectOfType<Zoomer>().ZeroPositionOut();
        });*/

        (receiver as Tourer).FreezeGUI();
        receiver.transform.position = (receiver as Tourer).TourerRoom.TeleportPoint.transform.position;
        GameObject.FindObjectOfType<Zoomer>().ZeroPositionOut();
    }
}
