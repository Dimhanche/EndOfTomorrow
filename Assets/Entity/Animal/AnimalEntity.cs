using System.Collections.Generic;
using UnityEngine;
using GOAP;
using UnityEngine.AI;

public class AnimalEntity : Entity
{
    public AnimalStat animalStats;
    public float cooldownDespawn = 1.5f;
    public float foodDetectionRadius = 5f;
    public FoodType foodType= FoodType.None;
    public float hunger = 0f;
    [HideInInspector]public NavMeshAgent navMeshAgent;
    [HideInInspector]public Vector3? wanderTarget;
    [HideInInspector]public GameObject foodTarget;
    protected bool isDead;

    #region GOAP
    protected static readonly GOAP_Planner planner = new GOAP_Planner();
    protected GOAP_Goal eatGoal;
    protected List<GOAP_Action> availableActions;
    protected Queue<GOAP_Action> plan;
    protected GOAP_Action currentAction;
    protected GOAP_Goal wanderGoal;

    protected virtual void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = animalStats.speed;
    }
    protected virtual void Update()
    {
        if (isDead)
            return;
    }
    #endregion

    public override void TakeDamage(int pdamage,Entity caster,int armorValue)
    {
        base.TakeDamage(pdamage, caster, armorValue);
        animalStats.currentLife = (pdamage) > 0 ? animalStats.currentLife - (pdamage) : animalStats.currentLife;
        if(animalStats.currentLife <= 0)
        {
            Die(caster);
        }
    }

    protected override void Die(Entity caster = null)
    {
        base.Die(caster);
        navMeshAgent.isStopped = true;
        navMeshAgent.velocity = Vector3.zero;
        navMeshAgent.ResetPath();
        navMeshAgent.enabled = false;
        isDead = true;
        if(caster != null)
            caster.GetComponent<PlayerLeveling>().AddExperience(animalStats.experienceDrop);
        GetComponent<Lootable>().enabled = true;
        GetComponent<Renderer>().material = deadMaterial;
        GetComponent<Lootable>().onLooted.AddListener(() => { Destroy(gameObject, cooldownDespawn); });
        enabled = false;
    }

    private void OnDestroy()
    {
        GetComponent<Lootable>().onLooted.RemoveAllListeners();
    }

    public void OnEat()
    {
        Destroy(foodTarget);
    }
}

public enum FoodType
{
    None,
    Plant,
    Meat
}
