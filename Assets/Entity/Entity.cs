using System;
using UnityEngine;

[Serializable]
public class Entity
{
        public string entityName;
        public SOStats entityStats;
        public float experienceDrop;
        public int nbEnemyKill;
        public int nbDeath;
        public int money;

        public void TakeDamage(int damage)
        {
            entityStats.health -= damage;
            if(entityStats.health <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            entityStats.health = 0;
            Debug.Log("Die");
        }
}
