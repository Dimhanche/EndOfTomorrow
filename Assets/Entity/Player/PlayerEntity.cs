using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerEntity : EntityInfo
{
    public float baseDamage = 10;
    public float workSpeed = 10;
    public int reputation;
    public int competencePoint;

    public bool canMove;
    public bool canInterract;

    public bool isDead;
    public bool isPaused;

    private UIWindow[] _windows;

    [HideInInspector]public UnityEvent lifeChanged = new UnityEvent();
    [HideInInspector]public UnityEvent<int,int> xpChanged = new UnityEvent<int,int>();

    public static PlayerEntity Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        lifeChanged.AddListener(OnLifeChanged);
        xpChanged.AddListener(OnXpChanged);
        _windows = FindObjectsByType<UIWindow>(FindObjectsSortMode.None);
    }

    public void OnXpChanged(int currentExperience, int nextLevelExperience)
    {
        GetComponentInChildren<PlayerXpDisplayer>().UpdateXpBar(currentExperience, nextLevelExperience);
    }
    public void OnLifeChanged()
    {
        GetComponentInChildren<PlayerLifeDisplayer>().UpdateLifeBar( entity.entityStats.currentLife,entity.entityStats.maxLife);
    }
    
    public void PlayerOpenPauseMenuInput(InputAction.CallbackContext cxt)
    {
        if (!cxt.performed) return;

        if (WindowManager.CheckAllWindowClose())
        {
            isPaused = !isPaused;
            if(isPaused)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
        else
        {
            WindowManager.CloseAllWindow();
        }
    }
}
