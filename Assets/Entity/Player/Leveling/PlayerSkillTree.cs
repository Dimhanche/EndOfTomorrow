
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerSkillTree : MonoBehaviour
{
    public UIWindow skillTreeWindow;
    private Vector2 _initialMousePosition;
    private bool _isDragging;
    private RectTransform _nodeParent;
    private PlayerInput _playerInput;

    private void Start()
    {
        _nodeParent = skillTreeWindow.transform.GetComponentInChildren<RectMask2D>().transform.GetChild(0).GetComponent<RectTransform>();
        _playerInput = GetComponent<PlayerInput>();
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
        if (!skillTreeWindow.CheckOpened() || Mouse.current == null)
        {
            _isDragging = false;
            return;
        }

        if (!_playerInput.actions.FindAction("Click").IsPressed())
        {
            _isDragging = false;
            return;
        }

        Vector2 mousePosition = Mouse.current.position.ReadValue();

        if (!_isDragging)
        {
            _isDragging = true;
            _initialMousePosition = mousePosition;
            return;
        }

        MoveWindow(mousePosition);
    }

    private void MoveWindow(Vector2 currentMousePosition)
    {
        Vector2 difference = currentMousePosition - _initialMousePosition;
        _nodeParent.position += (Vector3)difference;
        _initialMousePosition = currentMousePosition;
    }
}