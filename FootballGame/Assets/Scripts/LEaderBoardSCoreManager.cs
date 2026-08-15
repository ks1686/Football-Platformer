using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class LEaderBoardSCoreManager : MonoBehaviour
{
   [SerializeField]
   private TextMeshProUGUI inputScore;
   [SerializeField]
   private TextMeshProUGUI inputName;

   public UnityEvent<string, int> submitScoreEvent;
   
   public void SubmitScore()
   {
      string username = ScoreRules.SanitizeUsername(inputName != null ? inputName.text : null);
      if (!ScoreRules.IsValidUsername(username))
      {
         Debug.LogWarning("Leaderboard submit ignored: username is empty.");
         return;
      }

      string scoreText = inputScore != null ? inputScore.text : string.Empty;
      if (!ScoreRules.TryParseScore(scoreText, out int score))
      {
         score = PlayerPrefs.GetInt(ScoreRules.FinalScoreKey, 0);
      }

      submitScoreEvent.Invoke(username, score);
   }
}
