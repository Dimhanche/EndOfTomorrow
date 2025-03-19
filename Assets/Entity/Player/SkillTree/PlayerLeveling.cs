
using System.Collections.Generic;
using UnityEngine;

public class PlayerLeveling : MonoBehaviour
{
    public int nextLevelExperience;
    public int currentLevel;
    public int currentExperience;
    public int quadraticExperienceMultiplayer;
    public int multiplier1;
    public int multiplier2;


    public void AddExperience(int experience)
    {
        currentExperience += experience;
        if (currentExperience >= nextLevelExperience)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        currentLevel++;
        currentExperience = 0;
        CalculateNextLevelExperience();
    }

    private void CalculateNextLevelExperience()
    {
        if (currentLevel <= 10)
        {
            nextLevelExperience = quadraticExperienceMultiplayer * currentLevel * currentLevel;
        }
        else
        {
            nextLevelExperience = (int)(multiplier1 + multiplier2  * Mathf.Sqrt(currentLevel-10)) ;
        }
    }
}
