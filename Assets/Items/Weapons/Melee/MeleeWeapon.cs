using Random = UnityEngine.Random;

public class MeleeWeapon : Weapon
{
    public MeleeWeaponItem weaponItem;

    public int CalculateCrit()
    {
        if(Random.Range(0,100) < weaponItem.critRate)
            return weaponItem.critDamage;
        return 0;
    }
}
