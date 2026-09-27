using UnityEngine;

public class WeaponItem : Item
{
    [Header("Weapon Stats")]
    public int damage;
    public float attackSpeed;
    public float range;


    public virtual void Attack(ref float cooldownAttack,int baseDamage,Entity entity,ItemStack stack)
    {
        cooldownAttack = attackSpeed;
        RaycastHit hit;
        if(Physics.Raycast(Camera.main!.transform.position, Camera.main.transform.forward, out hit, range,LayerMask.GetMask("Default")))
        {
            if(hit.collider.TryGetComponent(out Entity entityScoped))
            {
                if (!entityScoped.isActiveAndEnabled)
                    return;
                Debug.Log("Shooting " + entityScoped.name);
                entityScoped.TakeDamage(CalculateDamage(baseDamage),entity,entityScoped.GetComponent<EntityEquipment>().GetArmorValue());
            }
        }
    }
    protected virtual int CalculateDamage(int baseDamage)
    {
        return damage + baseDamage;
    }
}
