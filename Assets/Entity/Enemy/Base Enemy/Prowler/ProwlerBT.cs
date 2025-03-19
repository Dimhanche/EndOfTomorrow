using System.Collections.Generic;
using BehaviorTree;


public class ProwlerBT : Tree
{
    public UnityEngine.Transform[] waypoints;
    private float _speed => GetComponent<EnemyEntity>().entity.entityStats.speed;
    private float _attackRange => GetComponent<EnemyEntity>().attackRange;
    private int _damage => GetComponent<EnemyEntity>().damage;
    private float _fovRange => GetComponent<EnemyEntity>().fovRange;
    private float _attackSpeed => GetComponent<EnemyEntity>().attackSpeed;
    private float _timeForSearching => GetComponent<EnemyEntity>().timeForSearching;

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckEntityInAttackRange(transform,_attackRange),
                new AttackTask(transform,this.GetComponent<EntityInfo>(),_damage,_attackSpeed),
            }),

            new Sequence(new List<Node>
            {
                new CheckInRangeTask(transform,_fovRange),
                new GoToTargetTask(transform,_speed),
            }),

            new PatrolTask(transform, waypoints, _speed),

        });
        return root;
    }
}
