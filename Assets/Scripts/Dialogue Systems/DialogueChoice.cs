using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class DialogueChoice : MonoBehaviour
{
    public string text { get { return inputField.text; } }
    public Dialogue nextDialogue;

    private TMP_InputField inputField;

    private void Start()
    {
        this.inputField = GetComponentInChildren<TMP_InputField>();
    }
}
