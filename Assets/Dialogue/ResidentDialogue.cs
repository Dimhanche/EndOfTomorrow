using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResidentDialogue : MonoBehaviour, IInteract
{
    public UIWindow dialogueWindow;
    public bool questGived;
    public GameObject responsePrefab;
    public Transform responseParent;
    public DialogueData currentDialogue;
    public bool canTalk = true;
    public bool isShow;

    private int _currentIndex = 0;
    private DisplayDialogue _displayDialogue;
    public int indexCheckQuest;
    public int indexAfterQuest;
    bool _questCompleted;
    private bool _questFailed;

    private void Start()
    {
        _displayDialogue = dialogueWindow.GetComponent<DisplayDialogue>();
    }

    public void Interact(ref float cooldownMax)
    {
        if (!canTalk) return;

        if (!questGived || _questCompleted)
        {
            HandleDialogue();
        }
        else
        {
            HandleQuest();
        }

        cooldownMax = 0.2f;
    }

    private void HandleDialogue()
    {
        if (!dialogueWindow.CheckOpened())
        {
            dialogueWindow.Show();
            StartDialogue(currentDialogue);
        }
        else
        {
            _currentIndex++;
            ShowDialogue();
        }
    }

    private void HandleQuest()
    {
        if (!dialogueWindow.CheckOpened())
        {
            dialogueWindow.Show();
        }

        var quest = currentDialogue.dialogueEntries[0].questToAdd;
        quest.CheckQuest(PlayerEntity.Instance);
        if (quest.isCompleted)
        {
            ShowDialogue(currentDialogue.dialogueEntries[indexCheckQuest + 1]);
            _questCompleted = true;
            _currentIndex = indexAfterQuest;
            PlayerEntity.Instance.GetComponent<PlayerQuest>().RemoveQuest(quest);
        }
        else
        {
            if(_questFailed)
            {
                EndDialogue();
                _questFailed = false;
            }
            else
            {
                ShowDialogue(currentDialogue.dialogueEntries[indexCheckQuest]);
                _questFailed = true;
            }
        }
    }

    public void StartDialogue(DialogueData dialogue)
    {
        currentDialogue = dialogue;
        if (_questCompleted)
            _currentIndex = indexAfterQuest;
        ShowDialogue();
    }

    private void ShowDialogue()
    {
        if (_currentIndex >= currentDialogue.dialogueEntries.Count || currentDialogue.dialogueEntries[_currentIndex].isLast)
        {
            EndDialogue();
            return;
        }

        var entry = currentDialogue.dialogueEntries[_currentIndex];
        DisplayEntry(entry);
    }

    private void ShowDialogue(DialogueData.DialogueEntry entry)
    {
        DisplayEntry(entry);
    }

    private void DisplayEntry(DialogueData.DialogueEntry entry)
    {
        _displayDialogue.Display(entry.dialogueText, GetComponent<EntityInfo>().entity.entityName);
        ClearOldChoices();
        AddNewChoices(entry.choices);
    }

    private void ClearOldChoices()
    {
        foreach (Transform child in responseParent)
        {
            Destroy(child.gameObject);
        }
    }

    private void AddNewChoices(List<DialogueData.DialogueChoice> choices)
    {
        foreach (var choice in choices)
        {
            var buttonObj = Instantiate(responsePrefab, responseParent);
            var button = buttonObj.GetComponent<ResponseButton>();
            button.SetResponse(choice.choiceText, this);
            button.GetComponent<Button>().onClick.AddListener(() => ChooseOption(choice));
        }
    }

    private void ChooseOption(DialogueData.DialogueChoice choice)
    {
        switch (choice.actionType)
        {
            case DialogueActionType.OpenShop:
                OpenShop();
                return;
            case DialogueActionType.AddQuest:
                AddQuest();
                break;
            case DialogueActionType.GiveItem:
                Debug.Log("Give item");
                break;
        }

        if (choice.nextDialogueId == -1)
        {
            EndDialogue();
        }
        else
        {
            _currentIndex = choice.nextDialogueId;
            ShowDialogue();
        }
    }

    private void OpenShop()
    {
        canTalk = false;
        GetComponent<Merchant>().OpenShop();
        EndDialogue();
    }

    private void AddQuest()
    {
        var questToAdd = currentDialogue.dialogueEntries[0].questToAdd;
        if (questToAdd != null)
        {
            PlayerEntity.Instance.GetComponent<PlayerQuest>().AddQuest(questToAdd);
        }

        questGived = true;
    }

    private void EndDialogue()
    {
        dialogueWindow.Close();
        _displayDialogue.Display("", "");
    }
}