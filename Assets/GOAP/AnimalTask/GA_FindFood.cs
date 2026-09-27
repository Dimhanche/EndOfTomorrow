
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

    public override GOAP_State Perform(EntityInfo entity)
    {
        if (entity is not AnimalEntity animalEntity)
        {
            Debug.Log("Entity is not an animal.");
            return GOAP_State.Failure;;
        }
        GameObject nearestFood = Utils.Utils.FindNearest(animalEntity.transform, animalEntity.foodDetectionRadius,animalEntity.foodType.ToString());
        if (nearestFood)
        {
            animalEntity.foodTarget = nearestFood;
            Debug.Log("Food found: " + nearestFood.name);
            return GOAP_State.Success;
        }
        Debug.Log("No food found or entity is not an animal.");
        return GOAP_State.Failure;
    }

    public override bool CheckProceduralPrecondition(EntityInfo entity)
    {
        return true;
    }
}
