using System;
using UnityEngine;
using UnityEngine.UI;

public class NodeCompetence : MonoBehaviour
{
    public SONodeCompetencePoint soNode;
    private Button _nodeButton;
    public Image nodeImagePrefabs;
    private Entity _entity;

    private void Start()
    {
        _nodeButton = GetComponent<Button>();
        _nodeButton.onClick.AddListener(UnlockNode);
        _entity = GetComponentInParent<EntityInfo>().entity;
        CheckNextNodes();
    }

    public void UnlockNode()
    {
        if (soNode.isUnlocked)
        {
            return;
        }

        if (soNode.competencePointCost <= _entity.competencePoint)
        {
            if (soNode.soNodeRequired != null)
            {
                if (soNode.soNodeRequired.isUnlocked)
                {
                    UnlockButton();
                    _entity.competencePoint -= soNode.competencePointCost;
                }
            }
            else
            {
                UnlockButton();
                _entity.competencePoint -= soNode.competencePointCost;
            }
        }
    }

    public void UnlockButton()
    {
        soNode.isUnlocked = true;
        _nodeButton.interactable = false;
    }

    private void GenerateArrow(Vector3 nextNode)
    {
        Image nodeImage = Instantiate(nodeImagePrefabs, transform);

        Vector3 anchorPos = transform.position;

        Vector3 currentPos = nextNode;

        currentPos.z = 0;

        Vector3 midPointVector = (currentPos + anchorPos) / 2;

        nodeImage.transform.position = midPointVector;

        Vector3 relative = currentPos - anchorPos;
        float maggy = relative.magnitude;


        nodeImage.transform.localScale = new Vector3(maggy / 2, 1, 0);
        Quaternion rotationVector = Quaternion.LookRotation(relative);
        rotationVector.z = 0;
        rotationVector.w = 0;
        nodeImage.transform.rotation = rotationVector;
    }

    private void CheckNextNodes()
    {
        if (soNode.nextNodes.Length == 0)
        {
            return;
        }

        foreach (NodeCompetence node in transform.parent.GetComponentsInChildren<NodeCompetence>())
        {
            foreach (SONodeCompetencePoint nextNode in soNode.nextNodes)
            {
                if (node.soNode == nextNode)
                {
                    Debug.Log(nextNode.name);
                    GenerateArrow(node.transform.position);
                }
            }
        }
    }
}