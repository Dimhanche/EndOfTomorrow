using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Stat", menuName = "ScriptableObjects/Stats", order = 1)]
public class SOStats : ScriptableObject
{
    public float health;
    public float healthRegen;
    public float damage;
    public float armor;
    public float speed;
    public float attackSpeed;
    public float luck;
    public float attackRange;
    public float jumpForce;
}