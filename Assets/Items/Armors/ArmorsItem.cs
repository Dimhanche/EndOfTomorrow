using UnityEngine;
using UnityEngine.Serialization;

public enum EArmorType
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
    [FormerlySerializedAs("armorType")] public EArmorType eArmorType;
}
