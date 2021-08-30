using GooglePlayGames.BasicApi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause_menu : MonoBehaviour
{
    public Save_manager save_manager;
    public GameObject optionMenu;
    public GameObject pausePanel;
    public GameObject continueButton;
    public GameObject shopButton;
    public GameObject rankingButton;
    public GameObject soundButton;
    public GameObject loadButton;
    public GameObject saveButton;
    public GameObject languageButton;
    public GameObject quitButton;
    public GameObject sureToQuit;

    // 게임 데이터
    string[]          game_data = new string[6];
    // 사운드 관련
    public GameObject sound_image_object;
    public Sprite     sound_on_sprite;
    public Sprite     sound_off_sprite;
    Image             audio_image;
    int               sound_type;
    // 기타 관련
    public GameObject ms_seller_object;


    private void Start()
    {
        Init_settings();
        Init_game_data();
        Hide_all_buttons();
        Set_sound();
    }

    void Init_settings()
    {
        if (!PlayerPrefs.HasKey("Sound"))
            sound_type = 1;

        else
            sound_type = PlayerPrefs.GetInt("Sound");

        audio_image = sound_image_object.GetComponent<Image>();
        sureToQuit.SetActive(false);
    }

    // 저장 데이터 초기화
    void Init_game_data()
    {
        game_data[0] = System.IO.Path.Combine(Application.persistentDataPath, "/player_menu_data.json");
        game_data[1] = System.IO.Path.Combine(Application.persistentDataPath, "/crop_menu_data.json");
        game_data[2] = System.IO.Path.Combine(Application.persistentDataPath, "/city_menu_data.json");
        game_data[3] = System.IO.Path.Combine(Application.persistentDataPath, "/resident_menu_data.json");
        game_data[4] = System.IO.Path.Combine(Application.persistentDataPath, "/automation_menu_data.json");
        game_data[5] = System.IO.Path.Combine(Application.persistentDataPath, "/building_menu_data.json");
    }

    public void OnClick()
    {
        Tutorial_manager.step = 1;
        Audio_manager.instance.Play_touch_sound();
        Show_all_buttons();
    }
    public void Resume()
    {
        Tutorial_manager.step = 4;
        Audio_manager.instance.Play_touch_sound();
        Hide_all_buttons();
    } 
    public void Quit()
    {
        Audio_manager.instance.Play_touch_sound();
        sureToQuit.SetActive(true);
    }
    public void sureToQuitYes()
    {
        Audio_manager.instance.Play_touch_sound();
        Application.Quit();
    }
    public void sureToQuitNo()
    {
        Audio_manager.instance.Play_touch_sound();
        Hide_all_buttons();
        sureToQuit.SetActive(false);
    }

    // 저장된 게임 불러오기
    public void Load_game()
    {
        Audio_manager.instance.Play_touch_sound();
        GooglePlayServiceManager.Instance.LogIn();
        byte[] save_game_data = PlayCloudDataManager.Instance.ToBytes();
        PlayCloudDataManager.Instance.LoadFromCloud();
        PlayCloudDataManager.Instance.ProcessCloudData(save_game_data);
        save_manager.Load_data_from_file();
    }

    // 게임 저장하기
    public void Save_game()
    {
        Audio_manager.instance.Play_touch_sound();
        GooglePlayServiceManager.Instance.LogIn();

        for(int i = 0; i < game_data.Length; i++)
        {
            PlayCloudDataManager.Instance.MergeWith(game_data[i]);
        }
        PlayCloudDataManager.Instance.SaveToCloud();
    }
    
    // 소리 관련 함수
    public void Set_sound()
    {
        switch (sound_type)
        {
            case 0:
                audio_image.sprite = sound_off_sprite;
                Audio_manager.instance.Set_sound_off();
                PlayerPrefs.SetInt("Sound", 0);
                sound_type = 1;
                break;

            case 1:
                audio_image.sprite = sound_on_sprite;
                Audio_manager.instance.Set_sound_on();
                PlayerPrefs.SetInt("Sound", 1);
                sound_type = 0;
                break;
        }
    }

    public void Hide_all_buttons()
    {
        //Time.timeScale = 1f;
        optionMenu.SetActive(false);
        pausePanel.SetActive(false);
        continueButton.SetActive(false);
        shopButton.SetActive(false);
        rankingButton.SetActive(false);
        soundButton.SetActive(false);
        saveButton.SetActive(false);
        loadButton.SetActive(true);
        languageButton.SetActive(false);
        quitButton.SetActive(false);
        ms_seller_object.SetActive(true);
    }
    public void Show_all_buttons()
    {
        //Time.timeScale = 0f;
        optionMenu.SetActive(true);
        pausePanel.SetActive(true);
        continueButton.SetActive(true);
        shopButton.SetActive(true);
        rankingButton.SetActive(true);
        soundButton.SetActive(true);
        saveButton.SetActive(true);
        loadButton.SetActive(true);
        languageButton.SetActive(true);
        quitButton.SetActive(true);
        ms_seller_object.SetActive(false);
    }
}