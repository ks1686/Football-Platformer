using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;
using System;

public class ScoarBoard : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> names;
    [SerializeField]
    private List<TextMeshProUGUI> scores;

    private string publicLeaderboardKey = 
    "76b2bfdb2f901cbaa058f9fbe4bb0a3d4a4c8eaf3d6b041af601dcb470c59d86";

    private void Start()
    {
     GetLeaderboard();   
    }
    public void GetLeaderboard(){
       LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) => {
           int loopLength = (msg.Length < names.Count) ? msg.Length : names.Count;
           for (int i = 0; i < loopLength; ++i){
            names[i].text = msg[i].Username;
            scores[i].text = msg[i].Score.ToString();
           }
       }), (error) => {
           Debug.LogWarning("Leaderboard fetch failed: " + error);
       });
    }

    public void SetLeaderboardEntry(String username, int score){
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username,
        score, ((msg) => {
            GetLeaderboard();
        }), (error) => {
            Debug.LogWarning("Leaderboard upload failed: " + error);
        });
    }
}
