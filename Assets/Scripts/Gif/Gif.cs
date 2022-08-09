using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gif : MonoBehaviour
{
    public Texture[] frames;
    public int framesPerSecond = 10;
    
    private void Update() 
    {
        int index = (int) (Time.time * framesPerSecond) % frames.Length;
        GetComponent<Renderer>().material.mainTexture = frames[index];
    }
}
