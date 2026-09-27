using UnityEngine;

public class Entity : MonoBehaviour
{
    public Material deadMaterial;

    /// <summary>
    /// This method is called when the entity takes damage.
    /// </summary>
    /// <param name="pdamage">Damage Taken</param>
    /// <param name="caster">Damage Caster</param>
    /// <param name="armorValue">Current Armor Value</param>
    public virtual void TakeDamage(int pdamage,Entity caster,int armorValue)
    {
        Debug.Log($"Take damage {pdamage} from {caster.name} with armor value {armorValue}");
    }

    protected virtual void Die(Entity caster = null)
    {
        if(caster != null)
            Debug.Log($"{name} has died by {caster.name}");
    }

}