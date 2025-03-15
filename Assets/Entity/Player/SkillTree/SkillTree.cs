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
        // Display the skill tree

    }
}
