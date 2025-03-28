using System;
using UnityEngine;
using UnityEngine.UI;

public class NodeCompetence : MonoBehaviour
{
    public SONodeCompetencePoint soNode;
    private Button _nodeButton;
    public Image nodeImagePrefabs;
    private PlayerEntity _entity;

    private void Start()
    {
        _nodeButton = GetComponent<Button>();
        _nodeButton.onClick.AddListener(UnlockNode);
        _entity = GetComponentInParent<PlayerEntity>();
        CheckNextNodes();

        if (soNode.isUnlocked)
        {
            UnlockButton();
        }
    }

    public void UnlockNode()
    {
        if (soNode.isUnlocked || soNode.competencePointCost <= _entity.competencePoint)
        {
            return;
        }

        if (soNode.soNodeRequired.Length > 0)
        {
            foreach (SONodeCompetencePoint requiredNode in soNode.soNodeRequired)
            {
                if (!requiredNode.isUnlocked)
                {
                    return;
                }
            }
        }
        UnlockButton();
        _entity.competencePoint -= soNode.competencePointCost;
    }

    public void UnlockButton()
    {
        soNode.isUnlocked = true;
        _nodeButton.interactable = false;
    }

    private void GenerateArrow(Vector3 nextNode, float thickness)
    {
        Image nodeImage = Instantiate(nodeImagePrefabs, transform);

        Vector3 anchorPos = transform.position;
        Vector3 currentPos = nextNode;
        currentPos.z = 0;

        Vector3 midPointVector = (currentPos + anchorPos) / 2;
        nodeImage.transform.position = midPointVector;

        Vector3 relative = currentPos - anchorPos;
        float maggy = relative.magnitude;

        nodeImage.transform.localScale = new Vector3(maggy, thickness, 1);
        float angle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg;
        nodeImage.transform.rotation = Quaternion.Euler(0, 0, angle);
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
                    GenerateArrow(node.transform.position,4);
                }
            }
        }
    }
}