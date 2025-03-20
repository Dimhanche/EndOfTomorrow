using Unity.VisualScripting;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    private EntityInfo _entityInfo;
    private Stats _stats;
    private float _armorValue => GetComponent<EntityEquipment>().GetArmorValue();

    private EntityInfo _lastCaster;

    private void Start()
    {
        _stats = GetComponent<EntityInfo>().entity.entityStats;
    }

    public void TakeDamage(int damage,EntityInfo lastCaster)
    {
        if(lastCaster != _lastCaster)
            _lastCaster = lastCaster;
        _stats.currentLife = (damage - _armorValue) > 0 ? _stats.currentLife - (damage - _armorValue) : _stats.currentLife;
        if(TryGetComponent(out PlayerEntity player))
        {
            player.lifeChanged.Invoke();
        }
        if(_stats.currentLife <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        _stats.currentLife = 0;
        if(TryGetComponent(out PlayerEntity player))
        {
            player.canMove = false;
        }
        else if(TryGetComponent(out EnemyEntity enemy))
        {
            _lastCaster.GetComponent<PlayerLeveling>().AddExperience(enemy.entity.experienceDrop);
            Destroy(gameObject);
        }
    }
}
