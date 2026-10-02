using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id = -1;

    public virtual void Attack(ref float cooldownAttack,int baseDamage,Entity entity)
    {
        //OnAttack.Invoke(baseDamage,entity);
    }

    protected virtual int CalculateDamage(int baseDamage,float distance = 0)
    {
        return baseDamage;
    }
}
