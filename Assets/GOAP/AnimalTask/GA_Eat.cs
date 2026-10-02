
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

    public override EGOAP_State Perform(Entity entity)
    {
       Debug.Log("Je mange !");
        if (entity is not AnimalEntity animalEntity)
        {
            return EGOAP_State.Failure;
        }
        animalEntity.hunger = animalEntity.animalStats.maxHunger;
        animalEntity.OnEat();
        return EGOAP_State.Success;
    }

    public override bool CheckProceduralPrecondition(Entity entity)
    {
        return true;
    }
}
