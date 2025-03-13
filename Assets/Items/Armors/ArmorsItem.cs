using UnityEngine;

public enum ArmorType
{
    Helmet,
    Chestplate,
    Leggings,
    Belt,
    Gauntlet
}

[CreateAssetMenu(fileName = "New Armor", menuName = "Items/Armors")]
public class ArmorsItem : Item
{
    [Header("Armor Stats")]
    public int defense;
    public ArmorType armorType;
}
