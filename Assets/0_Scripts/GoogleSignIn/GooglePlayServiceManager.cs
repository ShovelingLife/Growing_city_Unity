using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
public class GooglePlayServiceManager : Singleton_global<GooglePlayServiceManager>
{
    public bool isAuthenticated
    {
        get{ return Social.localUser.authenticated; }
    }

    private void Start()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
                Debug.Log("Login Success");

            else
                Debug.Log("Login Failed");
        });
        if (GooglePlayServiceManager.instance.isAuthenticated)
            GooglePlayServiceManager.instance.Complete_first_login();
    }

    public void Log_in()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = false;
        PlayGamesPlatform.Activate();
    }

    public void Show_leader_board()
    {
        if (!isAuthenticated)
        {
            Log_in();
            return;
        }
        Social.ShowLeaderboardUI();
    }
    public void Complete_first_login()
    {
        if (!isAuthenticated)
        {
            Log_in();
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