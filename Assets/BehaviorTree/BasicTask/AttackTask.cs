using UnityEngine;
using BehaviorTree;
public class AttackTask : Node
{
    private Transform _lastTarget;
    private LifeManager _lifeManager;

    private float _attackTimer;
    private float _attackCounter = 0;
    private int _damage;

    public AttackTask(Transform transform, int damage = 10, float attackSpeed = 1.0f)
    {
        this._damage = damage;
        this._attackTimer = attackSpeed;
        _attackCounter = _attackTimer;
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
            _lifeManager.TakeDamage(_damage);
            _attackCounter = 0;
        }

        state = NodeState.RUNNING;
        return state;
    }
}
