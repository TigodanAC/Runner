using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayHitAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("TakeDamage");
        }
    }

    public void PlayPickupAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("PickupBonus");
        }
    }

    public void SetInvincible(bool isInvincible)
    {
        if (animator != null)
        {
            animator.SetBool("IsInvincible", isInvincible);
        }
    }

}