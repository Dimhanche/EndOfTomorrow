using UnityEngine;
using BehaviorTree;
using JetBrains.Annotations;

public class GoToTargetTask : Node
{
    private Transform _transform;

    private float _speed;

    public GoToTargetTask(Transform transform,float speed = 5.0f)
    {
        _transform = transform;
        _speed = speed;
    }

    public override ENodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        _transform.position = Vector3.MoveTowards(_transform.position, target.position, Time.deltaTime*_speed);
        _transform.LookAt(target.position);
        state = ENodeState.RUNNING;
        return state;
    }
}