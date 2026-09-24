using UnityEngine;

public class EntityInfo : MonoBehaviour
{
    public Entity entity;

    public virtual void TakeDamage(int pdamage,EntityInfo caster,int armorValue)
    {
        Debug.Log($"Take damage {pdamage} from {caster.name} with armor value {armorValue}");
    }


    protected virtual void Die(EntityInfo caster)
    {
        entity.entityStats.currentLife = 0;
    }
}