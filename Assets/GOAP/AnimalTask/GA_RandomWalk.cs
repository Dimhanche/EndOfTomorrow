using GOAP;
using UnityEngine;
using UnityEngine.AI;

public class GA_RandomWalk : GOAP_Action
{
    const float Radius = 10f;

    public GA_RandomWalk()
    {
        Name = "Random Walk";
        Effects.Add("HasWandered", true);
        Cost = 1;
    }

    public override EGOAP_State Perform(Entity entity)
    {
        if (entity is not AnimalEntity animal)
            return EGOAP_State.Failure;

        NavMeshAgent agent = animal.navMeshAgent;

        if (animal.wanderTarget == null)
        {
            Vector2 c = Random.insideUnitCircle * Radius;
            Vector3 candidate = animal.transform.position + new Vector3(c.x, 0f, c.y);
            if (!NavMesh.SamplePosition(candidate, out NavMeshHit hit, 2f, NavMesh.AllAreas))
                return EGOAP_State.Failure;

            animal.wanderTarget = hit.position;
            agent.SetDestination(hit.position);
        }

        if (!agent.pathPending)
        {
            if (agent.pathStatus == NavMeshPathStatus.PathInvalid)
            {
                animal.wanderTarget = null;
                return EGOAP_State.Failure;
            }

            if (agent.remainingDistance <= Mathf.Max(agent.stoppingDistance, 0.5f))
            {
                animal.wanderTarget = null;
                agent.ResetPath();
                return EGOAP_State.Success;
            }
        }

        return EGOAP_State.Running;
    }

    public override bool CheckProceduralPrecondition(Entity entity) => true;
}