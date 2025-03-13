using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Items/Weapons")]
public class WeaponsItem : Item
{
    [Header("Weapon Stats")]
    public int damage;
    public float attackSpeed;
    public float range;
    public bool isOneHanded;
}
