using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public WeaponItem currentWeapon => GetComponent<PlayerEquipment>().weapon;
    private ItemStack currentWeaponStack => GetComponent<PlayerEquipment>().weaponStack;
    public float cooldownAttack;
    public float cooldownReload;
    private bool _canMove => GetComponent<PlayerEntity>().canMove;

    public void PlayerAttackInput(InputAction.CallbackContext ctx)
    {
        if(ctx.performed && currentWeapon && _canMove && cooldownAttack <= 0)
        {
            Attack();
        }
    }

    public void PlayerReloadInput(InputAction.CallbackContext ctx)
    {
        if(ctx.performed && currentWeapon is RangeWeaponItem rangeWeapon && _canMove && cooldownReload <= 0)
        {
            // TODO: once bullet ItemStacks exist, bail out here if the inventory has no matching
            // bullets left to reload with (ItemStack.Reload() will need them to actually refill).
            currentWeaponStack.Reload();
            cooldownReload = rangeWeapon.reloadTime;
        }
    }

    private void Attack()
    {
        currentWeapon.Attack(ref cooldownAttack, (int)GetComponent<PlayerEntity>().baseDamage, GetComponent<EntityInfo>(), currentWeaponStack);
    }

    private void Update()
    {
        if(cooldownAttack > 0)
        {
            cooldownAttack -= Time.deltaTime;
        }
        if(cooldownReload > 0)
        {
            cooldownReload -= Time.deltaTime;
        }
    }
}
