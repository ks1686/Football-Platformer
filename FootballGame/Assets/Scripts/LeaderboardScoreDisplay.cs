using UnityEngine;
using TMPro;          // Use this for TextMeshPro

public class LeaderboardScoreDisplay : MonoBehaviour
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
        finalScoreTextElement.text = finalScore.ToString(System.Globalization.CultureInfo.InvariantCulture);
    }
}
