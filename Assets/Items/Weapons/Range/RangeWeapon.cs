using System;
using UnityEngine;

public class RangeWeapon : Weapon
{
    public RangeWeaponItem weaponItem;
    [field: SerializeField]public float ammountAmmo { protected set; get; }

    private void Awake()
    {
        ammountAmmo = weaponItem.ammoCapacity;
    }

    public override void Attack(ref float cooldownAttack,int baseDamage,Entity entity)
    {
        cooldownAttack = weaponItem.attackSpeed;
        if(ammountAmmo <= 0)
        {
            Reload();
            return;
        }
        ammountAmmo--;
        if(Physics.Raycast(Camera.main!.transform.position, Camera.main.transform.forward,out RaycastHit hit, weaponItem.range,LayerMask.GetMask("Default")))
        {
            if(hit.collider.TryGetComponent(out Entity entityScoped))
            {
                if (!entityScoped.isActiveAndEnabled)
                    return;
                Debug.Log("Shooting " + entityScoped.name);
                entityScoped.TakeDamage(CalculateDamage(baseDamage),entity,entityScoped.GetComponent<EntityEquipment>().GetArmorValue());
            }
        }
        base.Attack(ref cooldownAttack,baseDamage,entity);
    }

    public void Reload()
    {
        ammountAmmo = weaponItem.ammoCapacity;
    }

    protected override int CalculateDamage(int baseDamage,float distance = 0)
    {
        return (int)((weaponItem.damage + baseDamage)*weaponItem.damagePerDistance);
    }
}
