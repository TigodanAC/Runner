using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    [HideInInspector] public int currentHealth;

    private bool isDead = false;
    private bool isInvincible = false;
    private Coroutine invincCoroutine = null;

    private PlayerAnimationController animController;

    void Start()
    {
        currentHealth = Mathf.Clamp(currentHealth == 0 ? maxHealth : currentHealth, 0, maxHealth);
        UIManager.Instance?.UpdateHealth(currentHealth, maxHealth);
        animController = GetComponent<PlayerAnimationController>();
    }

    public void ApplyDamage(int amount)
    {
        if (isDead) return;
        if (isInvincible) return;
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UIManager.Instance?.UpdateHealth(currentHealth, maxHealth);

        if (animController != null)
        {
            animController.PlayHitAnimation();
        }

        if (currentHealth <= 0) Die();
    }

    public void Heal(int amount)
    {
        if (isDead) return;

        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UIManager.Instance?.UpdateHealth(currentHealth, maxHealth);

        if (animController != null)
        {
            animController.PlayPickupAnimation();
        }
    }

    public void MakeInvincible(float seconds)
    {
        if (invincCoroutine != null)
            StopCoroutine(invincCoroutine);
        invincCoroutine = StartCoroutine(Invincibility(seconds));
    }

    IEnumerator Invincibility(float seconds)
    {
        isInvincible = true;
        if (animController != null)
            animController.SetInvincible(true);
        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateInvincibility(isInvincible);
        }

        yield return new WaitForSeconds(seconds);

        isInvincible = false;
        if (animController != null)
            animController.SetInvincible(false);
        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateInvincibility(isInvincible);
        }

        invincCoroutine = null;
    }

    void Die()
    {
        isDead = true;
        GameManager.Instance?.OnPlayerDeath();
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit == null || hit.collider == null) return;

        Obstacle obstacle = hit.collider.GetComponentInParent<Obstacle>()
                            ?? hit.collider.GetComponentInChildren<Obstacle>()
                            ?? hit.collider.GetComponent<Obstacle>();

        if (obstacle != null)
        {
            if (obstacle.Hit())
            {
                ApplyDamage(obstacle.damage);
            }
        }
    }
}