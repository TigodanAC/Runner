using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [Header("Score Settings")]
    public int pointsPerSecond = 1;
    public TextMeshProUGUI scoreText;

    private int currentScore = 0;
    private float timeCounter = 0f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {

        UpdateScoreDisplay();
    }

    void Update()
    {
        timeCounter += Time.deltaTime;

        if (timeCounter >= 1f)
        {
            timeCounter -= 1f;
            AddScore(pointsPerSecond);
        }
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        UpdateScoreDisplay();
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }

    public void ResetScore()
    {
        currentScore = 0;
        timeCounter = 0f;
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {currentScore}";
        }
    }
}