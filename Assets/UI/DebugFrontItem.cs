using System;
using TMPro;
using UnityEngine;

public class DebugFrontItem : MonoBehaviour
{

    public TextMeshProUGUI text;
    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 5,LayerMask.GetMask("Default")))
        {
                text.text = hit.collider.gameObject.name;
        }
        else
        {
            text.text = "";
        }
    }
}
