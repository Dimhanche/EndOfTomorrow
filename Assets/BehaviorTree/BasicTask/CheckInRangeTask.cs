using UnityEngine;
using BehaviorTree;

public class CheckInRangeTask : Node
{
    private Transform _transform;
    private float _fovRange;

    public CheckInRangeTask(Transform transform, float fovRange = 10.0f)
    {
        _transform = transform;
        _fovRange = fovRange;
    }

    public override NodeState Evaluate()
    {
        object target = GetData("target");
        Collider[] colliders = Physics.OverlapSphere(_transform.position, _fovRange, LayerMask.GetMask("Player"));
        if (colliders.Length > 0)
        {
            parent.parent.SetData("target", colliders[0].transform);
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;

    }
}