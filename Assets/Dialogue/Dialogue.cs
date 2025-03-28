using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueData
{
    [System.Serializable]
    public class DialogueEntry
    {
        [TextArea(3, 10)] public string dialogueText;
        public List<DialogueChoice> choices;
        public bool isLast = false;
        public Quest questToAdd;
    }

    [System.Serializable]
    public class DialogueChoice
    {
        public string choiceText;
        public int nextDialogueId;
        public EDialogueActionType actionType;
    }

    public List<DialogueEntry> dialogueEntries;
}

public enum EDialogueActionType
{
    None,
    OpenShop,
    AddQuest,
    GiveItem
}