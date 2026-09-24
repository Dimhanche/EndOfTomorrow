using UnityEngine;

public class EnemyEntity : EntityInfo
{
    public float attackRange => GetComponent<EnemyEquipment>().weapon.range;
    public int damage => GetComponent<EnemyEquipment>().weapon.damage;
    public float fovRange = 10.0f;
    public float attackSpeed => GetComponent<EnemyEquipment>().weapon.attackSpeed;
    public float timeForSearching = 5.0f;

    public override void TakeDamage(int pdamage,EntityInfo caster,int armorValue)
    {
        base.TakeDamage(pdamage, caster, armorValue);
        Stats stats = entity.entityStats;
        stats.currentLife = (pdamage - armorValue) > 0 ? stats.currentLife - (pdamage - armorValue) : stats.currentLife;
        if(stats.currentLife <= 0)
        {
            Die(caster);
        }
    }

    protected override void Die(EntityInfo caster)
    {
        base.Die(caster);
        caster.GetComponent<PlayerLeveling>().AddExperience(this.entity.experienceDrop);
        Destroy(gameObject);
    }
}
