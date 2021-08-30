using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Advertisements;

public class Banner_ad : MonoBehaviour
{
    public string banner_placement = "GameScreen";


    private void Update()
    {
        Show_ad();
    }

    public void Show_ad()
    {
        if (PlayerPrefs.GetInt("ad_bought") == 1)
            return;

        Advertisement.Initialize(Global.game_ID);
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        StartCoroutine(IE_show_banner_when_ready());
    }

    IEnumerator IE_show_banner_when_ready()
    {
        while (!Advertisement.IsReady(banner_placement))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.Show(banner_placement);
    }
}