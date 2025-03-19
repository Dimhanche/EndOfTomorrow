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

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t == null)
        {
            state = NodeState.FAILURE;
            return state;
        }
        Transform target = (Transform)t;
        if (Vector3.Distance(_transform.position, target.position) < _attackRange)
        {


            state = NodeState.SUCCESS;
            return state;
        }
        state = NodeState.FAILURE;
        return state;
    }
}
