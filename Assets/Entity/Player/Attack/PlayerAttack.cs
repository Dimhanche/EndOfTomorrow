using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public WeaponsItem currentWeapon => GetComponent<PlayerEquipment>().weapon;
    public float cooldownAttack;
    private bool _canMove => GetComponent<PlayerEntity>().canMove;

    public void PlayerAttackInput(InputAction.CallbackContext ctx)
    {
        if(ctx.performed && currentWeapon && _canMove && cooldownAttack <= 0)
        {
            Attack();
        }
    }

    private void Attack()
    {
        cooldownAttack = currentWeapon.attackSpeed;
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, currentWeapon.range))
        {
            if(hit.collider.TryGetComponent(out EntityInfo entity))
            {
                entity.GetComponent<LifeManager>().TakeDamage(CalculateDamage(),this.GetComponent<EntityInfo>());
            }
        }
    }

    private void Update()
    {
        if(cooldownAttack > 0)
        {
            cooldownAttack -= Time.deltaTime;
        }
    }

    private int CalculateDamage()
    {
        return currentWeapon.damage;
    }
}
