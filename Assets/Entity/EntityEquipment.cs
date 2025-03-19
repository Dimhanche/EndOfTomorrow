using UnityEngine;

public class EntityEquipment : MonoBehaviour
{
    public WeaponsItem weapon;
    public ArmorsItem[] armor;

    public int GetArmorValue()
    {
        int armorValue = 0;
        foreach (ArmorsItem item in armor)
        {
            if (item != null)
            {
                armorValue += item.defense;
            }
        }
        return armorValue;
    }
}
