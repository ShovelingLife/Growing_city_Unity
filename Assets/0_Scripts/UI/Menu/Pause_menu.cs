using GooglePlayGames.BasicApi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause_menu : MonoBehaviour
{
    // 전체 메뉴들을 가져옴
    [SerializeField]
    GameObject[]      m_arr_main_menu_background;
    [SerializeField]
    GameObject[]      m_arr_menu;

    // 현재 쓰여질 함수들
    Dictionary<int, Action> m_dic_menu_functions = new Dictionary<int, Action>();

    // 게임 데이터
    string[]          m_arr_game_data = new string[6];

    // 사운드 관련
    public Image      audio_image;
    public Sprite     sound_on_sprite;
    public Sprite     sound_off_sprite;
    bool              m_is_sound_on;

    // 기타 관련
    public GameObject ms_seller_object;

    private void Awake()
    {
        Init_game_data();
        Init_main_menu_background();
        Init_option_buttons();
    }

    private void Start()
    {
        Init_settings();
        Hide_all_buttons();
        Set_sound();
    }

    void Init_settings()
    {
        if (!PlayerPrefs.HasKey("Sound"))
            m_is_sound_on = true;

        else
            m_is_sound_on = Convert.ToBoolean(PlayerPrefs.GetInt("Sound").ToString());
    }

    // 메인 메뉴 배경화면을 초기화
    void Init_main_menu_background()
    {
        // 메인 메뉴 버튼 초기화 후 끄기
        m_arr_main_menu_background = new GameObject[transform.childCount];

        for (int i = 0; i < m_arr_main_menu_background.Length; i++)
        {
            int index = Convert.ToInt32(transform.GetChild(i).gameObject.name.Split('_')[0]);
            m_arr_main_menu_background[i] = transform.GetChild(index - 1).gameObject;
            m_arr_main_menu_background[i].SetActive(false);
        }
    }

    // 옵션 메뉴 버튼들을 초기화
    void Init_option_buttons()
    {
        // 메뉴 이벤트 설정
        // 0 = 재개
        // 1 = 캐시샵
        // 2 = 업적
        // 3 = 저장
        // 4 = 불러오기
        // 5 = 언어
        // 6 = 종료
        // 7 = 소리

        m_dic_menu_functions.Add(0, Resume);
        m_dic_menu_functions.Add(1, Shop_manager.instance.Open_shop_menu);
        m_dic_menu_functions.Add(2, GooglePlayServiceManager.instance.Show_leader_board);
        m_dic_menu_functions.Add(3, Save_game);
        m_dic_menu_functions.Add(4, Load_game);
        m_dic_menu_functions.Add(5, m_arr_main_menu_background[1].GetComponent<Language_menu>().Open_language_menu);
        m_dic_menu_functions.Add(6, Quit);
        m_dic_menu_functions.Add(7, Set_sound);

        // 메뉴 버튼 초기화
        Transform buttons_parent = transform.GetChild(0);
        m_arr_menu = new GameObject[buttons_parent.childCount];

        for (int i = 0; i < m_arr_menu.Length; i++)
        {
            // 오브젝트 설정 후 이벤트 설정
            m_arr_menu[i] = buttons_parent.GetChild(i).gameObject;
            m_arr_menu[i].GetComponent<Button>().onClick.AddListener(m_dic_menu_functions[i].Invoke);
        }
    }

    // 저장 데이터 초기화
    void Init_game_data()
    {
        m_arr_game_data[0] = System.IO.Path.Combine(Application.persistentDataPath, "/player_menu_data.json");
        m_arr_game_data[1] = System.IO.Path.Combine(Application.persistentDataPath, "/crop_menu_data.json");
        m_arr_game_data[2] = System.IO.Path.Combine(Application.persistentDataPath, "/city_menu_data.json");
        m_arr_game_data[3] = System.IO.Path.Combine(Application.persistentDataPath, "/resident_menu_data.json");
        m_arr_game_data[4] = System.IO.Path.Combine(Application.persistentDataPath, "/automation_menu_data.json");
        m_arr_game_data[5] = System.IO.Path.Combine(Application.persistentDataPath, "/building_menu_data.json");
    }

    void Open_menu()
    {
        Tutorial_manager.step = 1;
        Audio_manager.instance.Play_touch_sound();
        Show_all_buttons();
    }

    void Resume()
    {
        Tutorial_manager.step = 4;
        Audio_manager.instance.Play_touch_sound();
        Hide_all_buttons();
    } 

    void Quit()
    {
        Audio_manager.instance.Play_touch_sound();
        m_arr_main_menu_background[2].SetActive(true);
    }

    public void Sure_to_quit_yes()
    {
        Audio_manager.instance.Play_touch_sound();
        Application.Quit();
    }

    public void Sure_to_quit_no()
    {
        Audio_manager.instance.Play_touch_sound();
        Hide_all_buttons();
        m_arr_main_menu_background[2].SetActive(false);
    }

    // 저장된 게임 불러오기
    void Load_game()
    {
        Audio_manager.instance.Play_touch_sound();
        GooglePlayServiceManager.instance.Log_in();
        byte[] arr_save_game_data = PlayCloudDataManager.Instance.ToBytes();
        PlayCloudDataManager.Instance.LoadFromCloud();
        PlayCloudDataManager.Instance.ProcessCloudData(arr_save_game_data);
        Save_manager.instance.Load_data_from_file();
    }

    // 게임 저장하기
    void Save_game()
    {
        Audio_manager.instance.Play_touch_sound();
        GooglePlayServiceManager.instance.Log_in();

        for(int i = 0; i < m_arr_game_data.Length; i++)
            PlayCloudDataManager.Instance.MergeWith(m_arr_game_data[i]);

        PlayCloudDataManager.Instance.SaveToCloud();
    }
    
    // 소리 관련 함수
    void Set_sound()
    {
        if(!m_is_sound_on)
        {
            audio_image.sprite = sound_off_sprite;
            Audio_manager.instance.Set_sound_off();
            PlayerPrefs.SetInt("Sound", 0);
            m_is_sound_on = true;
        }
        else
        {
            audio_image.sprite = sound_on_sprite;
            Audio_manager.instance.Set_sound_on();
            PlayerPrefs.SetInt("Sound", 1);
            m_is_sound_on = false;
        }
    }

    public void Hide_all_buttons()
    {
        //Time.timeScale = 1f;
        for (int i = 0; i < m_arr_menu.Length; i++)
             m_arr_menu[i].SetActive(false);

        m_arr_main_menu_background[0].SetActive(false);
        ms_seller_object.SetActive(true);
    }
    public void Show_all_buttons()
    {
        //Time.timeScale = 0f;
        for (int i = 0; i < m_arr_menu.Length; i++)
             m_arr_menu[i].SetActive(true);

        m_arr_main_menu_background[0].SetActive(true);
        ms_seller_object.SetActive(false);
    }
}