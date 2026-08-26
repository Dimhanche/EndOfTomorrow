using UnityEngine;

// Hidden, self-created host that gives the static WindowManager a place to run
// once per frame. LateUpdate specifically: the frame's input dispatch is over by
// then, so enabling or disabling an action map here can never feed the press
// that is being handled back into an action. See WindowManager for the details.
//
// Spawned by WindowManager.Bootstrap - nothing references this from a scene.
public class WindowManagerRunner : MonoBehaviour
{
    private void LateUpdate()
    {
        WindowManager.ApplyPendingState();
    }
}
