using UnityEngine;
using UnityEngine.UI; // Required for standard UI Text
using TMPro; // Required for TextMeshPro UGUI

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance { get; private set; } // Singleton

    public float timeLimit = 60f; // Time limit in seconds (e.g., 1 minute)
    public TextMeshProUGUI timerText;

    private float currentTime;
    private bool timeIsUp = false;
    private bool isRunning = true;

    void Awake()
    {
         // Implement the Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentTime = timeLimit;
        timeIsUp = false;
        isRunning = true;
        UpdateTimerUI();
    }

    void Update()
    {
        if (isRunning && !timeIsUp)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0f)
            {
                currentTime = 0f;
                timeIsUp = true;
                isRunning = false;
                Debug.Log("Time's Up!");
                // Notify GameManager that time is up
                if (GameManager.Instance != null)
                {
                     GameManager.Instance.CheckLoseCondition();
                }
            }
            UpdateTimerUI();
        }
    }

    public bool IsTimeUp()
    {
        return timeIsUp;
    }

    public void StopTimer()
    {
         isRunning = false;
    }

     public void ResumeTimer()
    {
        if (!timeIsUp) // Don't resume if time already ran out
        {
            isRunning = true;
        }
    }


    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            // Format the time nicely (e.g., MM:SS)
            int minutes = Mathf.FloorToInt(currentTime / 60f);
            int seconds = Mathf.FloorToInt(currentTime % 60f);
            timerText.text = $"Time: {minutes:00}:{seconds:00}";
        }
         else
        {
            Debug.LogWarning("Timer Text UI element not assigned in TimerManager!");
        }
    }
}