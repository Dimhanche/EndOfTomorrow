using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = System.Random;

public class PlayerAttack : MonoBehaviour
{
    public WeaponItem currentWeapon => GetComponent<PlayerEquipment>().weapon;
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
        currentWeapon.Attack(ref cooldownAttack, (int)GetComponent<PlayerEntity>().baseDamage, GetComponent<EntityInfo>());
    }

    private void Update()
    {
        if(cooldownAttack > 0)
        {
            cooldownAttack -= Time.deltaTime;
        }
    }
}
