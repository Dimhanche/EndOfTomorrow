using System;
using UnityEngine;

[Serializable]
public class Entity
{
        public string entityName;
        public bool male;
        public Stats entityStats;
        public float experienceDrop;
        public int nbEnemyKill;
        public int nbDeath;
        public int money;
}
