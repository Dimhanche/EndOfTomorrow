using UnityEngine;
using UnityEngine.InputSystem;

public class SkillTree : MonoBehaviour
{
    public UIWindow skillTreeWindow;

    public void PlayerOpenSkillTreeInput(InputAction.CallbackContext cxt)
    {
        if (cxt.performed)
        {
            skillTreeWindow.Toggle();
            DisplaySkillTree();
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
}
