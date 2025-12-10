using UnityEngine;
using TMPro;
using System.Globalization;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI invincibilityText;

    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }

    public void UpdateHealth(int cur, int max)
    {
        if (healthText != null) healthText.text = $"Health: {cur}/{max}";
    }

    public void UpdateSpeed(float speed)
    {
        if (speedText != null)
            speedText.text = $"Player speed: {speed.ToString("F1", CultureInfo.InvariantCulture)}";
    }

    public void UpdateInvincibility(bool flag)
    {
        if (invincibilityText != null)
            invincibilityText.text = $"Invincibility: {flag}";
    }
}
