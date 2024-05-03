using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardScript : MonoBehaviour
{

    public void ShowLeaderboardUI()
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI();
    }


    public void AddHighScoreToLeaderboard(string leaderboard_id, int score)
    {
        PlayGamesPlatform.Instance.ReportScore(score, leaderboard_id, (bool success) =>
        {
            if (success)
            {
                Debug.Log("Score posted");
            }
            else
            {
                Debug.Log("Score was not posted");
            }
        });
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
