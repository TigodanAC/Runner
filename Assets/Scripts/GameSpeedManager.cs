using UnityEngine;
using System.Collections;

public class GameSpeedManager : MonoBehaviour
{
    public static GameSpeedManager Instance { get; private set; }

    public float startSpeed = 10f;
    public float stepAmount = 1f;
    public float stepInterval = 3f;
    public float maxSpeed = 40f;

    float currentSpeed;
    float timer = 0f;

    public float CurrentSpeed => currentSpeed;

    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }

    void Start()
    {
        currentSpeed = startSpeed;
        timer = 0f;

        if (UIManager.Instance != null)
            UIManager.Instance.UpdateSpeed(currentSpeed);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= stepInterval)
        {
            timer -= stepInterval;
            IncreaseSpeed();
        }
    }

    void IncreaseSpeed()
    {
        if (currentSpeed >= maxSpeed) return;

        currentSpeed = Mathf.Min(currentSpeed + stepAmount, maxSpeed);
        if (UIManager.Instance != null)
            UIManager.Instance.UpdateSpeed(currentSpeed);
    }
}
