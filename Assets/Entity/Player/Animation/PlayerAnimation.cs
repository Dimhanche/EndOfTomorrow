using NUnit.Framework.Constraints;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;


    public bool isWalking = false;



    public void SetPlayerAnimationWalking()
    {
        isWalking = true;
        UpdateAnimator("isWalking", isWalking);

    }

    public void SetPlayerAnimationIdling()
    {
        isWalking = false;
        UpdateAnimator("isWalking", isWalking);
    }

    public void SetPlayerAnimationJumping()
    {
        _animator.Play("Jumping");
    }

    private void UpdateAnimator(string condition,bool state)
    {
        _animator.SetBool(condition, state);
    }
}
