using UnityEngine;

public static class WindowManager
{
    public static int nbWindowsOpen = 0;

    public static bool OpenWindow()
    {
        nbWindowsOpen++;

        return CheckAllWindowClose();
    }

    public static bool CloseWindow()
    {
        nbWindowsOpen = Mathf.Max(0, nbWindowsOpen - 1);
        return CheckAllWindowClose();
    }

    public static bool CheckAllWindowClose()
    {
        if (nbWindowsOpen > 0)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            return false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            return true;
        }
    }

    public static void CloseAllWindow()
    {
        nbWindowsOpen = 0;
        CheckAllWindowClose();
    }
}