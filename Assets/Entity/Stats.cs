using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class Stats
{
    public float maxLife =100;
    public float currentLife =100;
    public float lifeRegen;
    public float speed =6;
    public float luck;
    public float jumpForce =4;
    public float oratory;
}