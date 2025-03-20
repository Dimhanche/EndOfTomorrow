
using System;
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

    private static PlayerEntity _playerEntity;

    private void Start()
    {
        _playerEntity = GetComponent<PlayerEntity>();
        AddExperience(0);
    }


    public void AddExperience(int experience)
    {
        currentExperience += experience;
        if (currentExperience >= nextLevelExperience)
        {
            LevelUp();
        }
        _playerEntity.xpChanged.Invoke(currentExperience, nextLevelExperience);
    }

    private void LevelUp()
    {
        currentExperience = (int)MathF.Max( currentExperience- nextLevelExperience, 0);
        currentLevel++;
        CalculateNextLevelExperience();
        _playerEntity.competencePoint = currentLevel%5 == 0 ? _playerEntity.competencePoint + 2 : _playerEntity.competencePoint + 1;
        if(currentExperience >= nextLevelExperience)
        {
            LevelUp();
        }
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
