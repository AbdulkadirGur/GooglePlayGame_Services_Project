using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;

public class Achievement : MonoBehaviour
{
    public Text logtext;
    
    public void ShowAchievementsUI()
    {
        Social.ShowAchievementsUI();
    }
  
    public void DoGrantAchievement(string _achievement)
    {
        Social.ReportProgress(_achievement,
            100.00f,
            (bool success) =>
            {
                if (success) // if success is true 
                {
                    logtext.text = _achievement + " : " + success.ToString();
                    //perform new actions here on success
                }
                else
                {
                    logtext.text = _achievement + " : " + success.ToString();
                    //perform new actions here on failed
                }
            });

    }

    public void DoIncrementalAchievement(string _achievement)
    {
        PlayGamesPlatform platform = (PlayGamesPlatform)Social.Active;

        platform.IncrementAchievement(_achievement,
            1,
            (bool success) =>
            {
                if (success) // if success is true 
                {
                    logtext.text = _achievement + " : " + success.ToString();
                    //perform new actions here on success
                }
                else
                {
                    logtext.text = _achievement + " : " + success.ToString();
                    //perform new actions here on failed
                }
            });
    }


    public void DoRevealAchievement(string _achievement)
    {
        Social.ReportProgress(_achievement,
            0.00f,
            (bool success) =>
            {
                if (success) // if success is true 
                {
                    logtext.text = _achievement + " : " + success.ToString();
                    //perform new actions here on success
                }
                else
                {
                    logtext.text = _achievement + " : " + success.ToString();
                    //perform new actions here on failed
                }
            });

    }


    public void ListAchievements()
    {
        Social.LoadAchievements(achievements =>
        {
            logtext.text = "Loaded Achievements" + achievements.Length;
            foreach (IAchievement ach in achievements)
            {
                logtext.text += "/n" + ach.id + " " + ach.completed;
            }
        });
    }

    public void ListDescriptions()
    {
        Social.LoadAchievementDescriptions(achievements =>
        {
            logtext.text = "Loaded Achievements" + achievements.Length;
            foreach (IAchievementDescription ach in achievements)
            {
                logtext.text += "/n" + ach.id + " " + ach.title;
            }
        });
    }

    public void GrantAchievementBtn()
    {
        DoGrantAchievement(Code_Test.GPGSids.achievement_unlock_achievement);
    }

    public void GrantIncrementalBtn()
    {
        DoIncrementalAchievement(Code_Test.GPGSids.achievement_increment_achievement);
    }
    public void RevealAchievementBtn()
    {
        DoRevealAchievement(Code_Test.GPGSids.achievement_hidden_unlock_achievement);
    }

    public void RevealIncrementalBtn()
    {
        DoRevealAchievement(Code_Test.GPGSids.achievement_hidden_incremental_achievement);
    }

    public void GrantHiddenAchievementBtn()
    {
        DoGrantAchievement(Code_Test.GPGSids.achievement_hidden_unlock_achievement);
    }

    public void HiddenlIncrementalAchievementBtn()
    {
        DoIncrementalAchievement (Code_Test.GPGSids.achievement_hidden_incremental_achievement);
    }
}
