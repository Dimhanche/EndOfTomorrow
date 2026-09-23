using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Items/Weapons/Ranged")]
public class RangeWeaponItem : WeaponItem
{
    [Header("Range Spec")]
    public int rangeDistance;
    public int ammoCapacity;
    public float damagePerDistance;
    public float reloadTime;

    public override void Attack(ref float cooldownAttack, int baseDamage, EntityInfo entityInfo)
    {
        base.Attack(ref cooldownAttack, baseDamage, entityInfo);
    }
}
