using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;

public class Leaderboard : MonoBehaviour
{
    public Text logText;
    public InputField scoreInput;

    public void showLeaderboardUI()
    {
        Social.ShowLeaderboardUI();

    }

    public void DoLeaderboardPost(int _score)
    {
        Social.ReportScore(_score , Code_Test.GPGSids.leaderboard_test_leaderboards,
            (bool success) =>
            {
                if (success) // if success is true 
                {
                    logText.text = "Score Posted of: " + _score;
                    //perform new actions here on success
                }
                else
                {
                    logText.text = "Score Posted of: " + _score;
                    //logText.text = "Score Failed to Post";
                    //perform new actions here on failed
                }
            });
    }

    public void LeaderboardPostBtn()
    {
        DoLeaderboardPost(int.Parse(scoreInput.text));
    }
}
