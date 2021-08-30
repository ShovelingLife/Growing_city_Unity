using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
public class GooglePlayServiceManager : MonoBehaviour
{
    static GooglePlayServiceManager instance;

    public static GooglePlayServiceManager Instance
    {
        get
        {
            return instance;
        }
    }
    public bool isAuthenticated
    {
        get
        {
            return Social.localUser.authenticated;
        }
    }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    private void Start()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                Debug.Log("Login Success");
            }
            else if (!success)
            {
                Debug.Log("Login Failed");
            }
        });
        if (GooglePlayServiceManager.instance.isAuthenticated)
        {
            GooglePlayServiceManager.instance.completeFirstLogin();
        } 
    }

    public void LogIn()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = false;
        PlayGamesPlatform.Activate();
    }

    public void showLeaderboard()
    {
        if (!isAuthenticated)
        {
            LogIn();
            return;
        }
        Social.ShowLeaderboardUI();
    }
    public void completeFirstLogin()
    {
        if (!isAuthenticated)
        {
            LogIn();
            return;
        }
        PlayGamesPlatform.Activate();
        Social.ReportProgress(GPGSIds.achievement_first_login, 100.0,(bool success)=>
        {
            if (!success)
            {
                Debug.Log("Report Fail!");
            }
        });
    }
}