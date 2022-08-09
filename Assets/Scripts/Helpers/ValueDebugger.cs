using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ValueDebugger : MonoBehaviour
{
    public GameObject target;
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = target.transform.position.x + " " + target.transform.position.y + " " + target.transform.position.z;
    }
}
