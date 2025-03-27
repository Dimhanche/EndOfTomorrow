using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerEntity : EntityInfo
{
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
        lifeChanged.AddListener(OnLifeChaneged);
        xpChanged.AddListener(OnXpChanged);
        _windows = FindObjectsByType<UIWindow>(FindObjectsSortMode.None);


    }

    public void OnXpChanged(int currentExperience, int nextLevelExperience)
    {
        GetComponentInChildren<PlayerXpDisplayer>().UpdateXpBar(currentExperience, nextLevelExperience);
    }
    public void OnLifeChaneged()
    {
        GetComponentInChildren<PlayerLifeDisplayer>().UpdateLifeBar( entity.entityStats.currentLife,entity.entityStats.maxLife);
    }
    
    public void PlayerOpenPauseMenuInput(InputAction.CallbackContext cxt)
    {
        if (cxt.performed)
        {
            int nbWindowsOpen = 0;
            foreach (UIWindow window in _windows)
            {
                if(window.CheckOpened())
                {
                    nbWindowsOpen++;
                    window.Close();
                }
            }
            if(nbWindowsOpen == 0)
            {
                isPaused = !isPaused;
                if(isPaused)
                {
                    Time.timeScale = 0;
                    Cursor.lockState = CursorLockMode.Confined;
                    Cursor.visible = true;
                }
                else
                {
                    Time.timeScale = 1;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }
        }
    }
}
