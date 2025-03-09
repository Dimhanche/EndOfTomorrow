using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityInfo", menuName = "Entity/EntityInfo")]
public class SOEntity : ScriptableObject
{
        public string entityName;
        public SOStats entityStats;
        public GameObject model;
        public float experienceDrop;
        public int nbEnemyKill;
        public int nbDeath;
}
