using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR;

public class VTGDebug : MonoBehaviour
{
    private static int counter;

    public static void Log(string text)
    {
        var txt = GameObject.FindGameObjectWithTag("Logger");
        if (txt == null) return;
        var myText = txt.GetComponent<TextMeshProUGUI>();
        counter++;
        

        if (counter == 7)
        {
            counter = 0;
            myText.text = "";
        }

        myText.text += "[ " + text + " ]\n";
    }

    public static void ClearLog()
    {
        var txt = GameObject.FindGameObjectWithTag("Logger");
        if (txt == null) return;
        var myText = txt.GetComponent<TextMeshProUGUI>();
        myText.text += "";
    }
}
