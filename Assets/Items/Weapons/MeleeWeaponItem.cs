using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "New Melee Weapon", menuName = "Items/Weapons/Melee")]
public class MeleeWeaponItem : WeaponItem
{

    [Header("Melee Spec")]

    public float critRate;
    public int critDamage;
    public bool isOneHanded;


    public int CalculateCrit()
    {
        if(Random.Range(0,100) < critRate)
            return critDamage;
        return 0;
    }

    protected override int CalculateDamage(int baseDamage)
    {
            return damage + baseDamage + CalculateCrit();
    }
}

