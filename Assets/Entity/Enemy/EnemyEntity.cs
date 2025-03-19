using UnityEngine;

public class EnemyEntity : EntityInfo
{
    public float attackRange => GetComponent<EnemyEquipment>().weapon.range;
    public int damage => GetComponent<EnemyEquipment>().weapon.damage;
    public float fovRange = 10.0f;
    public float attackSpeed => GetComponent<EnemyEquipment>().weapon.attackSpeed;
    public float timeForSearching = 5.0f;
}
