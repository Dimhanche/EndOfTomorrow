using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public Weapon currentWeapon;
    public float cooldownAttack;
    public float cooldownReload;
    private bool _canMove => GetComponent<PlayerEntity>().canMove;

    public static UnityEvent<Weapon> OnAttackEvent = new UnityEvent<Weapon>();
    public static UnityEvent<Weapon> OnReloadEvent = new UnityEvent<Weapon>();

    public void PlayerAttackInput(InputAction.CallbackContext ctx)
    {
        if(ctx.performed && currentWeapon != null && _canMove && cooldownAttack <= 0)
        {
            Attack();
        }
    }

    public void PlayerReloadInput(InputAction.CallbackContext ctx)
    {
        if(ctx.performed && currentWeapon is RangeWeapon rangeWeapon && _canMove && cooldownReload <= 0)
        {
            // TODO: once bullet ItemStacks exist, bail out here if the inventory has no matching
            // bullets left to reload with (ItemStack.Reload() will need them to actually refill).
            rangeWeapon.Reload();
            cooldownReload = rangeWeapon.weaponItem.reloadTime;
            OnReloadEvent?.Invoke(currentWeapon);
        }
    }

    private void Attack()
    {
        currentWeapon.Attack(ref cooldownAttack, (int)GetComponent<PlayerEntity>().baseDamage, GetComponent<Entity>());
        OnAttackEvent?.Invoke(currentWeapon);
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
