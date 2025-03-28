using UnityEngine;
using BehaviorTree;
public class CheckEntityInAttackRange : Node
{
    private Transform _transform;
    private float _attackRange;

    public CheckEntityInAttackRange(Transform transform,float attackRange = 2.0f)
    {
        _transform = transform;
        _attackRange = attackRange;
    }

    public override ENodeState Evaluate()
    {
        object t = GetData("target");
        if (t == null)
        {
            state = ENodeState.FAILURE;
            return state;
        }
        Transform target = (Transform)t;
        if (Vector3.Distance(_transform.position, target.position) < _attackRange)
        {
            state = ENodeState.SUCCESS;
            return state;
        }
        state = ENodeState.FAILURE;
        return state;
    }
}
