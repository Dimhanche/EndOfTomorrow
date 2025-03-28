using UnityEngine;
using BehaviorTree;

public class PatrolTask : Node
{
    private Transform transform;
    private Transform[] waypoints;

    private int _currentWaypoint = 0;
    private float _speed;

    private bool _waiting = false;
    private float _waitTime = 1.0f;
    private float _waitCounter = 0;

    public PatrolTask(Transform transform,Transform[] waypoints,float speed = 1.0f)
    {
        this.transform = transform;
        this.waypoints = waypoints;
        this._speed = speed;
    }

    public override ENodeState Evaluate()
    {
        if (_waiting)
        {
            _waitCounter += Time.deltaTime;
            if (_waitCounter >= _waitTime)
            {
                _waiting = false;
                _waitCounter = 0;
            }
        }
        Transform wp = waypoints[_currentWaypoint];
        if (Vector3.Distance(transform.position, wp.position) < 0.1f)
        {
            transform.position = wp.position;
            _waitCounter = 0;
            _waiting = true;

            _currentWaypoint = (_currentWaypoint + 1) % waypoints.Length;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, wp.position, Time.deltaTime*_speed);
            transform.LookAt(wp.position);
        }

        state = ENodeState.RUNNING;
        return state;
    }
}
