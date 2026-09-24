using UnityEngine;

public class AnimalEntity : EntityInfo
{

    public override void TakeDamage(int pdamage,EntityInfo caster,int armorValue)
    {
        base.TakeDamage(pdamage, caster, armorValue);
        Stats stats = entity.entityStats;
        stats.currentLife = (pdamage) > 0 ? stats.currentLife - (pdamage) : stats.currentLife;
        if(stats.currentLife <= 0)
        {
            Die(caster);
        }
    }

    protected override void Die(EntityInfo caster)
    {
        base.Die(caster);
        caster.GetComponent<PlayerLeveling>().AddExperience(entity.experienceDrop);
        GetComponent<Lootable>().enabled = true;
        enabled = false;
    }
}
