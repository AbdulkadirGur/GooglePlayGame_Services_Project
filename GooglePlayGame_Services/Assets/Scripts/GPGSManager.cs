using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.UI;
using GooglePlayGames.Android;
using UnityEngine.SocialPlatforms;

using GooglePlayGames.BasicApi.SavedGame;
using System;

public class GPGSManager : MonoBehaviour
{
    private PlayGamesClientConfiguration clientConfigurations;
    public Text statusTxt;
    public Text descriptionTxt;
    public GameObject HomeBtnGo;
    public GameObject SignInBtn;

    public SavedGamesUI savedGamesUI;

    void Start()
    {
        ConfigureGPGS();
    }

    internal void ConfigureGPGS()
    {
        clientConfigurations = new PlayGamesClientConfiguration.Builder()
            .EnableSavedGames()
            .Build();
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

    #region Saved Game

    private bool isSaving;

    public void OpenSave(bool saving)
    {
        savedGamesUI.logTxt.text += "";
        savedGamesUI.logTxt.text += "Open Saved clicked";
        if (Social.localUser.authenticated)
        {
            savedGamesUI.logTxt.text += "User is authenticated";
            isSaving = saving;
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution("MyFileName", DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, SaveGameOpen);
        }
    }

    private void SaveGameOpen(SavedGameRequestStatus status, ISavedGameMetadata meta)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            if (isSaving) //are saying
            {
                //convert our datatypse to a byte array
                savedGamesUI.logTxt.text += "Status successful, attemting to save...";
                byte[] myData = System.Text.ASCIIEncoding.ASCII.GetBytes(GateSaveString());

                //update our metadata
                SavedGameMetadataUpdate updateForMetadata = new SavedGameMetadataUpdate.Builder().WithUpdatedDescription("I have updated my game at: " + DateTime.Now.ToString()).Build();

                //commit your save 
                ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(meta, updateForMetadata, myData, SaveCallBack);

            }
            else // are loading
            {
                savedGamesUI.logTxt.text += "Status successful, attemting to save...";
                ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(meta, LoadCallBack);
            }

        }
    }

    private void LoadCallBack(SavedGameRequestStatus status, byte[] data)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            savedGamesUI.logTxt.text += "load successful, attemting to read data";
            string loadedData = System.Text.ASCIIEncoding.ASCII.GetString(data);
            //Abdulkadir|33
            LoadSavingString(loadedData);
        }
    }

    public void LoadSavingString(string cloudData)
    {
        string[] cloudStringArr = cloudData.Split("|");
        //cloudStringArr[0]=="Abdulkadir";
        //cloudStringArr[1]=="33";

        savedGamesUI.name = cloudStringArr[0];
        savedGamesUI.age = int.Parse(cloudStringArr[1]);

        savedGamesUI.outPutTxt.text = "";
        savedGamesUI.outPutTxt.text += "My name is :"+ savedGamesUI.name;
        savedGamesUI.outPutTxt.text += "My age is :"+ savedGamesUI.age;
    }

    public string GateSaveString()
    {
        string datatoSave = "";

        datatoSave += savedGamesUI.name;        
        datatoSave += "|";
        datatoSave += savedGamesUI.age;
        //Abdulkadir|33
        return datatoSave;
    }

    private void SaveCallBack(SavedGameRequestStatus status, ISavedGameMetadata meta)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            savedGamesUI.logTxt.text += "file successfully saved to cloud.";
        }
        else
        {
            savedGamesUI.logTxt.text += "file failed saved to cloud.";
        }
    }
    #endregion
}
