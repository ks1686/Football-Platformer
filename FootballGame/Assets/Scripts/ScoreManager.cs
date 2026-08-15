using UnityEngine;
using UnityEngine.UI; // Required for standard UI Text
using TMPro; // Required for TextMeshPro UGUI

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int targetScore = ScoreRules.DefaultTargetScore;
    public TextMeshProUGUI scoreText;

    private int currentScore = 0;

    void Awake()
    {
        // Implement the Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    void Start()
    {

        currentScore = 0;
        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        UpdateScoreUI();
    }

    public bool HasReachedTarget()
    {
        return currentScore >= targetScore;
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = ScoreRules.FormatHudScore(currentScore);
        }
        else
        {
            Debug.LogWarning("Score Text UI element not assigned in ScoreManager!");
        }
    }

}