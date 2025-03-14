using UnityEngine;

public class LifeManager : MonoBehaviour
{
    private Stats _entityStats;
    private float _armorValue => GetComponent<PlayerEquipment>().GetArmorValue();

    private void Start()
    {
        _entityStats = GetComponent<EntityInfo>().entity.entityStats;
    }

    public void TakeDamage(int damage)
    {
        _entityStats.life = (damage - _armorValue) > 0 ? _entityStats.life - (damage - _armorValue) : _entityStats.life;
        if(_entityStats.life <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        _entityStats.life = 0;
        Debug.Log("Die");
    }
}
