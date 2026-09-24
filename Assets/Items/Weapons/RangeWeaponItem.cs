using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Items/Weapons/Ranged")]
public class RangeWeaponItem : WeaponItem
{
    [Header("Range Spec")]
    public int ammoCapacity;
    public float damagePerDistance;
    public float reloadTime;

    // TODO: once bullet types exist, add a field here (or on ItemStack) for the currently loaded
    // bullet type, and use its damage modifier in Attack below instead of a flat baseDamage.

    public override void Attack(ref float cooldownAttack, int baseDamage, EntityInfo entityInfo, ItemStack stack)
    {
        cooldownAttack = attackSpeed;
        if (stack.currentAmmo <= 0)
            return;

        stack.currentAmmo--;
        base.Attack(ref cooldownAttack, baseDamage, entityInfo, stack);
    }
}
