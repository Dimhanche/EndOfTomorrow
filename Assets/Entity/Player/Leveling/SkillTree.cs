using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    public UIWindow skillTreeWindow;
    private Vector3 _initialMousePosition;
    private bool _isDragging;
    private RectTransform _nodeParent;

    private void Start()
    {
        _nodeParent = skillTreeWindow.transform.GetComponentInChildren<RectMask2D>().transform.GetChild(0).GetComponent<RectTransform>();
    }

    public void PlayerOpenSkillTreeInput(InputAction.CallbackContext cxt)
    {
        if (cxt.performed)
        {
            skillTreeWindow.Toggle();
            DisplaySkillTree();
            _nodeParent.localPosition = Vector3.zero;
        }
    }

    private void DisplaySkillTree()
    {
        NodeCompetence[] nodesCompetences = GetComponentsInChildren<NodeCompetence>();
        foreach (NodeCompetence nodeComp in nodesCompetences)
        {
            if (nodeComp.soNode.isUnlocked)
            {
                nodeComp.UnlockButton();
            }
        }
    }

    private void Update()
    {
        if (skillTreeWindow.CheckOpened())
        {
            if (Input.GetMouseButtonDown(1))
            {
                _isDragging = true;
                _initialMousePosition = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(1))
            {
                _isDragging = false;
            }

            if (_isDragging)
            {
                MoveWindow();
            }
        }
    }

    private void MoveWindow()
    {
        Vector3 currentMousePosition = Input.mousePosition;
        Vector3 difference = currentMousePosition - _initialMousePosition;
        _nodeParent.position += difference;
        _initialMousePosition = currentMousePosition;
    }
}