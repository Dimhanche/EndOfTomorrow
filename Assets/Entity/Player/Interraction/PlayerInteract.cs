using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    public float cooldown = 0;

    public void PlayerInteractInput(InputAction.CallbackContext ctx)
    {
        if(ctx.performed && cooldown <= 0)
        {
            RaycastHit hit;
            if(Camera.main != null && Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 5f))
            {
                if(hit.collider.TryGetComponent(out IInteract interactable))
                {
                    interactable.Interact(ref cooldown);
                    Debug.Log("interract with " + hit.collider.name + " with cooldown " + cooldown);
                }
            }
        }
    }

    private void Update()
    {
        if(cooldown > 0)
        {
            cooldown-= Time.deltaTime;
        }
    }
}
