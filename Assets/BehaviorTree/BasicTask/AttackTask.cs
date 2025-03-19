using UnityEngine;
using BehaviorTree;
public class AttackTask : Node
{
    private Transform _lastTarget;
    private LifeManager _lifeManager;
    private EntityInfo _caster;

    private float _attackTimer;
    private float _attackCounter = 0;
    private int _damage;

    public AttackTask(Transform transform, EntityInfo caster,int damage = 10, float attackSpeed = 1.0f)
    {
        _damage = damage;
        _attackTimer = attackSpeed;
        _attackCounter = _attackTimer;
        _caster = caster;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        if (target != _lastTarget)
        {
            _lifeManager = target.GetComponent<LifeManager>();
            _lastTarget = target;
        }
        _attackCounter  += Time.deltaTime;
        if (_attackCounter >= _attackTimer)
        {
            _lifeManager.TakeDamage(_damage, _caster);
            _attackCounter = 0;
        }

        state = NodeState.RUNNING;
        return state;
    }
}
