using UnityEngine;


[CreateAssetMenu(fileName = "New Melee Weapon", menuName = "Items/Weapons/Melee")]
public class MeleeWeaponItem : WeaponItem
{

    [Header("Melee Spec")]

    public float critRate;
    public int critDamage;
    public bool isOneHanded;
}

