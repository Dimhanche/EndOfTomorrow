using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;

public class ResidentDialogue : MonoBehaviour,IInteract
{

    public UIWindow dialogueWindow;
    public Dialogue[] dialogues;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public bool isShow;
    private int _index = 0;

    public GameObject responsePrefab;
    public Transform responseParent;
    private bool _waitingForResponse = false;

    public bool canTalk = true;

    public void Interact(ref float cooldownMax)
    {
        if (canTalk)
        {
            if (!isShow)
            {
                _index = 0;
                StartDialogue();
                dialogueWindow.Show();
                isShow = true;
                nameText.text = GetComponent<EntityInfo>().entity.entityName;
            }
            else if (dialogues[_index-1].isLast && !_waitingForResponse)
            {
                EndDialogue();
            }
            else if(!_waitingForResponse)
            {
                ContinueDialogue();
            }
        }
        cooldownMax = 0.2f;
    }

    public void StartDialogue()
    {
        Debug.Log("Starting conversation with " + GetComponent<EntityInfo>().entity.entityName);
        dialogueText.text = dialogues[_index].sentence;
        _index++;
    }

    public void ContinueDialogue()
    {
        dialogueText.text = dialogues[_index].sentence;
        int currentIndex = _index;
        if(dialogues[_index].responses != null)
        {
            SpawnButtons(currentIndex);
        }
        _index++;

    }

    private void SpawnButtons(int currentIndex)
    {
        for (int i = 0; i < dialogues[currentIndex].responses.Length; i++)
        {
            GameObject answerButton = Instantiate(responsePrefab, responseParent);
            int currentWay = i;
            answerButton.GetComponent<ResponseButton>().SetResponse(dialogues[_index].responses[i], this);
            answerButton.GetComponent<Button>().onClick.AddListener(() => {
                _waitingForResponse = false;
                RemoveAllButton();
                if(dialogues[currentIndex].actions[currentWay] == DialogueAction.OpenShop)
                {
                    OpenShop();
                }
                if (dialogues[currentIndex].isLast)
                {
                    EndDialogue();
                }
                else
                {
                    ContinueDialogue();
                }


            });
            _waitingForResponse = true;

        }

    }

    private void OpenShop()
    {
        Debug.Log("Opening shop");
        canTalk = false;
        GetComponent<Merchant>().OpenShop();
    }

    public void EndDialogue()
    {
        dialogueWindow.Close();
        isShow = false;
    }

    public void RemoveAllButton()
    {
        for (int i = 0; i < responseParent.childCount; i++)
        {
            Destroy(responseParent.GetChild(i).gameObject);
        }
    }
}
