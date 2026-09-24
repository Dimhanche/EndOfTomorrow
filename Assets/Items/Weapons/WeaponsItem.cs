using UnityEngine;

public class WeaponItem : Item
{
    [Header("Weapon Stats")]
    public int damage;
    public float attackSpeed;
    public float range;


    public virtual void Attack(ref float cooldownAttack,int baseDamage,EntityInfo entityInfo,ItemStack stack)
    {
        cooldownAttack = attackSpeed;
        RaycastHit hit;
        if(Physics.Raycast(Camera.main!.transform.position, Camera.main.transform.forward, out hit, range,LayerMask.GetMask("Default")))
        {
            if(hit.collider.TryGetComponent(out EntityInfo entity))
            {
                if (!entity.isActiveAndEnabled)
                    return;
                Debug.Log("Shooting " + entity.name);
                entity.TakeDamage(CalculateDamage(baseDamage),entityInfo,entity.GetComponent<EntityEquipment>().GetArmorValue());
            }
        }
    }
    protected virtual int CalculateDamage(int baseDamage)
    {
        return damage + baseDamage;
    }
}
