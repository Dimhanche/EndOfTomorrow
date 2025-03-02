using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Entity", menuName = "ScriptableObjects/Enemy")]
public class SOEntity : ScriptableObject
{
        public string entityName;
        public SOStats entityStats;
        public GameObject model;
        public float experienceDrop;
        public int nbEnemyKill;
        public int nbDeath;
}
