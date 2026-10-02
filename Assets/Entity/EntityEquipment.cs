using System.Collections.Generic;
using UnityEngine;

public class EntityEquipment : MonoBehaviour
{
    public WeaponItem weapon;
    public ArmorsItem[] armor;

    public int GetArmorValue()
    {
        int armorValue = 0;
        foreach (ArmorsItem item in armor)
        {
            if (item)
            {
                armorValue += item.defense;
            }
        }
        return armorValue;
    }
}

