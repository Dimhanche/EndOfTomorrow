
using GOAP;
using UnityEngine;

public class GA_FindFood:GOAP_Action
{
    public GA_FindFood()
    {
        Name = "Find Food";
        Preconditions.Add("HasFoodTarget", false);
        Effects.Add("HasFoodTarget", true);
    }

    public override EGOAP_State Perform(Entity entity)
    {
        if (entity is not AnimalEntity animalEntity)
        {
            Debug.Log("Entity is not an animal.");
            return EGOAP_State.Failure;;
        }
        GameObject nearestFood = Utils.Utils.FindNearest(animalEntity.transform, animalEntity.foodDetectionRadius,animalEntity.foodType.ToString());
        if (nearestFood)
        {
            animalEntity.foodTarget = nearestFood;
            Debug.Log("Food found: " + nearestFood.name);
            return EGOAP_State.Success;
        }
        Debug.Log("No food found or entity is not an animal.");
        return EGOAP_State.Failure;
    }

    public override bool CheckProceduralPrecondition(Entity entity)
    {
        return true;
    }
}
