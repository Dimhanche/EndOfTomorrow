using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    public float coolDown = 0;

    public void PlayerInteractInput(InputAction.CallbackContext ctx)
    {
        if(ctx.performed && coolDown <= 0)
        {
            RaycastHit hit;
            if(Camera.main != null && Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 5f))
            {
                if(hit.collider.TryGetComponent(out IInteract interactable))
                {
                    interactable.Interact(ref coolDown);
                    Debug.Log("interract with " + hit.collider.name + " with cooldown " + coolDown);
                }
            }
        }
    }

    private void Update()
    {
        if(coolDown > 0)
        {
            coolDown-= Time.deltaTime;
        }
    }
}
