using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Items/Weapons/Ranged")]
public class RangeWeaponItem : WeaponItem
{
    [Header("Range Spec")]
    public int ammoCapacity;
    public float damagePerDistance;
    public float reloadTime;
}
