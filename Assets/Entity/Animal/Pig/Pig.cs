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
            { "IsNearFood", hasTarget && Vector3.Distance(transform.position, foodTarget.transform.position) <= animalStats.eatDistance },
            { "HasWandered", false }
        };
    }

    protected override void Awake()
    {
        base.Awake();
        hunger = animalStats.maxHunger;
        eatGoal = new GOAP_Goal("Eat", new Dictionary<string, bool> { { "IsHungry", false } });
        wanderGoal = new GOAP_Goal("Wander", new Dictionary<string, bool> { { "HasWandered", true } });
        availableActions = new List<GOAP_Action> { new GA_FindFood(), new GA_GoFood(), new GA_Eat(), new GA_RandomWalk() };
    }

    protected override void Update()
    {
        base.Update();
        hunger -= hungerRate * Time.deltaTime;
        if (hunger <= 0)
        {
            hunger = 0;
            Die();
            Destroy(gameObject, cooldownDespawn);
            return;
        }

        if (currentAction == null)
        {
            if (plan == null || plan.Count == 0)
            {
                var state = BuildWorldState();
                var goal = state["IsHungry"] ? eatGoal : wanderGoal;
                plan = planner.Plan(this, state, goal, availableActions);
                if (plan == null) return;
            }
            currentAction = plan.Dequeue();
        }

        switch (currentAction.Perform(this))
        {
            case EGOAP_State.Success:
                currentAction = null;
                break;
            case EGOAP_State.Failure:
                currentAction = null;
                plan = null;
                break;
        }
    }
}