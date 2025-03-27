using TMPro;
using UnityEngine;

public class DisplayDialogue : MonoBehaviour
{

    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI characterNameText;

    public void Display(string dialogue, string characterName)
    {
        dialogueText.text = dialogue;
        characterNameText.text = characterName;
    }
}
