using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class Reward_video : MonoBehaviour , IUnityAdsListener
{
    Button     m_button;
    string     m_placementId = "rewardedVideo";
    public int type_of_ad = 0;


    void Start()
    {
        m_button = GetComponent<Button>();

        // Set interactivity to be dependent on the Placement’s status:
        m_button.interactable = Advertisement.IsReady(m_placementId);

        // Map the ShowRewardedVideo function to the button’s click listener:
        if (m_button) 
            m_button.onClick.AddListener(Show_rewarded_video);

        // Initialize the Ads listener and service:
        Advertisement.AddListener(this);
        Advertisement.Initialize(Global.game_ID, true);
    }

    // Implement a function for showing a rewarded video ad:
    void Show_rewarded_video()
    {
        Advertisement.Show(m_placementId);
    }

    // Implement IUnityAdsListener interface methods:
    public void On_unity_ads_ready(string placementId)
    {
        // If the ready Placement is rewarded, activate the button: 
        if (placementId == m_placementId)
            m_button.interactable = true;
    }

    public void On_unity_ads_did_finish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            // Reward the user for watching the ad to completion.
            if      (type_of_ad == 1) 
                     Time_manager.instance.Set_gameplay_booster_values(false, 300f);

            else if (type_of_ad == 2) 
                     Reward_after_login.instance.Close_reward_gold_watching_ad();
        }
        else if (showResult == ShowResult.Skipped)
                 return;

        else
                 Debug.LogWarning("The Ad did not finish due to a error.");
    }
    public void On_unity_ads_did_error(string message)
    {
        // Log the error.
    }

    public void On_unity_ads_did_start(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }

    public void OnUnityAdsReady(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidError(string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        throw new System.NotImplementedException();
    }
}