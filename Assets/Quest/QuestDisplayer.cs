using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestDisplayer : MonoBehaviour
{
    [SerializeField]  private Image _questImage;
    [SerializeField]  private TextMeshProUGUI _questTitle;
    [SerializeField]  private TextMeshProUGUI _questDescription;

    public void DisplayQuest(Quest quest)
    {
        _questImage.sprite = quest.questSprite;
        _questTitle.text = quest.questTitle;
        _questDescription.text = quest.questDescription;
    }
}
