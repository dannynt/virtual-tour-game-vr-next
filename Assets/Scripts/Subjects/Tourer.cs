using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class Tourer : Subject
{
    public VRHands HandsWatcher { get; private set; }
    public Queue<VRInputType> Input;
    public Room TourerRoom { get; set; }

    protected override void Start()
    {
        base.Start();
        HandsWatcher = FindObjectOfType<VRHands>();
        Input = new Queue<VRInputType>();
        EnterState(new DefaultTourerState());
        //XRDevice.fovZoomFactor = 1.4f;
    }

    public void OnPrimaryButtonEvent(bool pressed)
    {
        /*if (pressed)
        {
            // Change room.
            TourerRoom = Object.FindObjectsOfType<Room>()[Random.Range(0, Object.FindObjectsOfType<Room>().Length)];
            Input.Enqueue(VRInputType.PRIMARY);
        }*/
    }

    public void Teleport(string roomTitle)
    {
        var rooms = Object.FindObjectsOfType<Room>();

        foreach (var room in rooms)
        {
            if (room.title == roomTitle)
            {
                TourerRoom = room;
                break;
            }
        }

        Input.Enqueue(VRInputType.TELEPORT);
    }

    public void FreezeGUI()
    {
        //var tourerGUI = GetComponentInChildren<TourerGUI>();
        //var rBody = tourerGUI.GetComponentInChildren<Rigidbody>();
        //rBody.velocity = Vector3.zero; 
        //rBody.constraints = RigidbodyConstraints.FreezeAll;
        //StartCoroutine(UnFreeze(tourerGUI));
    }

    private IEnumerator UnFreeze(TourerGUI tg)
    {
        yield return new WaitForSeconds(0.6f);
        //var rBody = tg.GetComponentInChildren<Rigidbody>();
        //rBody.constraints = RigidbodyConstraints.None;
        //rBody.velocity = Vector3.zero; 
    }
    
    private void Update() 
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Teleport("Band Room");
        }

        if (state == null) return;
        state.HandleInput(this);
    }
}
