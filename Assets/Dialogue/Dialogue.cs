using UnityEngine;

public enum DialogueAction
{
    None,
    OpenShop
}

[System.Serializable]
public class Dialogue
{
    public string sentence;
    public bool isLast;
    public string[] responses;
    public DialogueAction[] actions;
}
