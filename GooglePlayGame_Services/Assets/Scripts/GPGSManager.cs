using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.UI;

using GooglePlayGames.Android;


using UnityEngine.SocialPlatforms;

public class GPGSManager : MonoBehaviour
{
    private PlayGamesClientConfiguration clientConfigurations;
    public Text statusTxt;
    public Text descriptionTxt;
    public GameObject HomeBtnGo;
    public GameObject SignInBtn;

    void Start()
    {
        ConfigureGPGS();
    }

    internal void ConfigureGPGS()
    {
        clientConfigurations = new PlayGamesClientConfiguration.Builder().Build();
        SignIntoGPGS(SignInInteractivity.CanPromptOnce, clientConfigurations);
    }


    internal void SignIntoGPGS(SignInInteractivity interactivity, PlayGamesClientConfiguration configuration)
    {
        configuration = clientConfigurations;
        PlayGamesPlatform.InitializeInstance(configuration);
        PlayGamesPlatform.Activate();

        PlayGamesPlatform.Instance.Authenticate(interactivity, (code) =>
       {
           statusTxt.text = "Authenticating...";
           if (code == SignInStatus.Success)
           {
               statusTxt.text = "Successfully Authenticated";
               descriptionTxt.text = "Hello " + Social.localUser.userName + "You have an ID of " + Social.localUser.id;
               HomeBtnGo.SetActive(true);
               SignInBtn.SetActive(false);
           }
           else
           {
               statusTxt.text = "Failed to Authenticate";
               descriptionTxt.text = "Failed to Authenticate, reason for failure is " + code;

           }
       });

    }

    public void BasicSignInBtn()
    {
        SignIntoGPGS(SignInInteractivity.CanPromptAlways, clientConfigurations);
    }
    public void SignOutBtn()
    {
        PlayGamesPlatform.Instance.SignOut();
        statusTxt.text = "Signed Out ";
        descriptionTxt.text = "";
        HomeBtnGo.SetActive(false);
    }

}
