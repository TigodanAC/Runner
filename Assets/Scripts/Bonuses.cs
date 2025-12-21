using UnityEngine;

public enum BonusType
{
    Heal,
    Invincibility
}

[RequireComponent(typeof(Collider))]
public class Boneses : MonoBehaviour
{
    string playerTag = "Player";

    public BonusType bonusType = BonusType.Heal;
    public int healAmount = 1;
    public float invincibilitySeconds = 5f;
    public int bonusPoints = 1;

    void Start()
    {
        Destroy(gameObject, 30f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;

        PlayerHealth ph = other.GetComponent<PlayerHealth>() ?? other.GetComponentInParent<PlayerHealth>();

        switch (bonusType)
        {
            case BonusType.Heal:
                ph.Heal(healAmount);
                break;
            case BonusType.Invincibility:
                ph.MakeInvincible(invincibilitySeconds);
                break;
        }

        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(bonusPoints);
        }

        Debug.Log($"Bonus collected: {bonusType}");
        Destroy(gameObject);
    }
}
