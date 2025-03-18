using TMPro;
using UnityEngine;

public class ResponseButton : MonoBehaviour
{
    public void SetResponse(string response, ResidentDialogue residentDialogue)
    {
        GetComponentInChildren<TextMeshProUGUI>().text = response;
    }
}
