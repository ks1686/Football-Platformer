using UnityEngine;
using TMPro;          // Use this for TextMeshPro

public class EndScreenScore : MonoBehaviour
{
    public TextMeshProUGUI finalScoreTextElement; // For TextMeshPro Text

    void Start()
    {
        if (finalScoreTextElement == null)
        {
            Debug.LogError("[EndScreenUI] ERROR: The 'Final Score Text Element' has not been assigned in the Inspector! Cannot display score.");
            return;
        }

        int finalScore = PlayerPrefs.GetInt(ScoreRules.FinalScoreKey, 0);
        finalScoreTextElement.text = ScoreRules.FormatFinalScore(finalScore, ScoreRules.DefaultTargetScore);
    }
}