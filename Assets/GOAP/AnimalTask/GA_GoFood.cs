using GOAP;
using UnityEngine;
using UnityEngine.AI;

public class GA_GoFood: GOAP_Action
{
    public GA_GoFood()
    {
        Name = "Go To Food";
        Preconditions.Add("HasFoodTarget", true);
        Effects.Add("IsNearFood", true);
        Cost = 2;
    }
    public override EGOAP_State Perform(Entity entity)
    {
        if (entity is not AnimalEntity animalEntity)
        {
            return EGOAP_State.Failure;
        }
        NavMeshAgent agent = animalEntity.navMeshAgent;
        Vector3 target = animalEntity.foodTarget.transform.position;

        if (Vector3.Distance(animalEntity.transform.position, target) <= animalEntity.animalStats.eatDistance)
        {
            agent.ResetPath();
            return EGOAP_State.Success;
        }

        if (!agent.hasPath || Vector3.Distance(agent.destination, target) > 0.1f)
            agent.SetDestination(target);

        if (!agent.pathPending && agent.pathStatus == NavMeshPathStatus.PathInvalid)
            return EGOAP_State.Failure;

        return EGOAP_State.Running;
    }

    public override bool CheckProceduralPrecondition(Entity entity)
    {
        return true;
    }
}
