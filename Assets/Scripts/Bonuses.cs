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

        Destroy(gameObject);
    }
}
