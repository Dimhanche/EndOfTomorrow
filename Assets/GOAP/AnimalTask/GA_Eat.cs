
using GOAP;
using UnityEngine;

public class GA_Eat: GOAP_Action
{
    public GA_Eat()
    {
        Name = "Eat";
        Preconditions.Add("IsNearFood", true);
        Preconditions.Add("IsHungry", true);
        Effects.Add("IsHungry", false);
    }

    public override GOAP_State Perform(EntityInfo entity)
    {
       Debug.Log("Je mange !");
        if (entity is not AnimalEntity animalEntity)
        {
            return GOAP_State.Failure;
        }
        animalEntity.hunger = animalEntity.animalStats.maxHunger;
        animalEntity.OnEat();
        return GOAP_State.Success;
    }

    public override bool CheckProceduralPrecondition(EntityInfo entity)
    {
        return true;
    }
}
