using UnityEngine;

public class LifeManager : MonoBehaviour
{
    private Stats _entityStats;
    private float _armorValue => GetComponent<EntityEquipment>().GetArmorValue();

    private EntityInfo _lastCaster;

    private void Start()
    {
        _entityStats = GetComponent<EntityInfo>().entity.entityStats;
    }

    public void TakeDamage(int damage,EntityInfo lastCaster)
    {
        if(lastCaster != _lastCaster)
            _lastCaster = lastCaster;
        _entityStats.life = (damage - _armorValue) > 0 ? _entityStats.life - (damage - _armorValue) : _entityStats.life;
        if(_entityStats.life <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        _entityStats.life = 0;
        if(TryGetComponent(out PlayerEntity player))
        {
            player.canMove = false;
        }
        else if(TryGetComponent(out EnemyEntity enemy))
        {
            _lastCaster.GetComponent<PlayerEntity>().experience += enemy.entity.experienceDrop;
            Destroy(gameObject);
        }
    }
}
