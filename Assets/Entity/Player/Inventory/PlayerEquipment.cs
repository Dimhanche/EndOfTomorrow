using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    public WeaponsItem weapon;
    public ArmorsItem armor;
    public EntityInfo entityInfo;
    public bool isPlayer;

    private void Start()
    {
        entityInfo = GetComponent<EntityInfo>();
        isPlayer = entityInfo.isPlayer;
    }

    public void EquipWeapon(WeaponsItem newWeapon)
    {
        weapon = newWeapon;
    }

    public void EquipArmor(ArmorsItem newArmor)
    {
        armor = newArmor;
    }
}
