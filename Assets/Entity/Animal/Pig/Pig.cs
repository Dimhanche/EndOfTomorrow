using System.Collections.Generic;
using GOAP;
using UnityEngine;

public class Pig : AnimalEntity
{
    public float hungerRate = 1f;
    public float hungerThreshold = 50f;

    GOAP_WorldState BuildWorldState()
    {
        bool hasTarget = foodTarget != null;
        return new GOAP_WorldState
        {
            { "IsHungry", hunger < hungerThreshold },
            { "HasFoodTarget", hasTarget },
            { "IsNearFood", hasTarget && Vector3.Distance(transform.position, foodTarget.transform.position) <= animalStats.eatDistance }
        };
    }

    protected override void Awake()
    {
        base.Awake();
        hunger = animalStats.maxHunger;
        eatGoal = new GOAP_Goal("Eat", new Dictionary<string, bool> { { "IsHungry", false } });
        availableActions = new List<GOAP_Action> { new GA_FindFood(), new GA_GoFood(), new GA_Eat() };
    }

    protected override void Update()
    {
        hunger -= hungerRate * Time.deltaTime;
        if( hunger <= 0)
        {
            hunger = 0;
            Destroy(gameObject, cooldownDespawn);
            return;
        }
        if (currentAction == null)
        {
            if (plan == null || plan.Count == 0)
            {
                var state = BuildWorldState();
                if (!state["IsHungry"]) return;
                plan = planner.Plan(this, state, eatGoal, availableActions);
                if (plan == null) return;
            }
            currentAction = plan.Dequeue();
        }

        switch (currentAction.Perform(this))
        {
            case GOAP_State.Success:
                currentAction = null;
                break;
            case GOAP_State.Failure:
                currentAction = null;
                plan = null;
                break;
            default:
                break;
        }
    }
}