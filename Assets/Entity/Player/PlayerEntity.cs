using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerEntity : EntityInfo
{
    public int reputation;
    public int competencePoint;
    public int experience;

    public bool canMove;
    public bool canInterract;

    public bool isDead;
    public bool isPaused;
}
