using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerQuest : MonoBehaviour
{
    public UIWindow questBook;
    public GameObject questPrefab;
    public List<Quest> currentQuests = new List<Quest>(50);
    private Transform _questParent;

    private void Start()
    {
        _questParent = questBook.gameObject.GetComponentInChildren<VerticalLayoutGroup>().transform;
    }
    public void PlayerOpenQuestBookInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            questBook.Toggle();
            DisplayQuest();
        }
    }
    public void DisplayQuest()
    {
        DestroyAllQuests();
        foreach (Quest quest in currentQuests)
        {
            GameObject questObject = Instantiate(questPrefab, _questParent);
            QuestDisplayer questDisplayer = questObject.GetComponent<QuestDisplayer>();
            questDisplayer.DisplayQuest(quest);
        }
    }

    public void AddQuest(Sprite questSprite, string questTitle, string questDescription)
    {
        Quest quest = new Quest(questSprite,questTitle,questDescription);
        AddQuest(quest);
    }
    public void AddQuest(Quest quest)
    {
        currentQuests.Add(quest);
    }

    public void RemoveQuest(Quest quest)
    {
        currentQuests.Remove(quest);
    }

    public void RemoveQuest(int index)
    {
        currentQuests.RemoveAt(index);
    }
    private void DestroyAllQuests()
    {
        foreach (Transform child in _questParent)
        {
            Destroy(child.gameObject);
        }
    }
}