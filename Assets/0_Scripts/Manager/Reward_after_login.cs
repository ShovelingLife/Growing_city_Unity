using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;
using UnityEngine.UI;

public class Reward_after_login : Singleton_local<Reward_after_login>
{
    public GameObject ms_seller;
    // json 파일 불러오기 위함
    Resident_menu_data[]   m_resident_data    = new Resident_menu_data[6];
    Automation_menu_data[] m_automation_data  = new Automation_menu_data[6];
    Building_menu_data[]   m_building_data    = new Building_menu_data[4];
    public Button[]        menu_button        = new Button[4];
    public Button          move_to_next_map_button;

    // 기타 변수
    public Data_controller data_controller;
    Time_manager           m_time_manager;

    // 재접속 보상 관련
    public GameObject login_gold_object;
    public GameObject daily_reward_object;
    public Text       welcome_back_text;
    public Text       welcome_back_money_text;
    public Text       reward_x2_text;
    public Text       reward_gold_text;
    double            m_sum_of_all_per_sec;
    double            m_result_reward_after_time;


    private void Start()
    {
        login_gold_object.SetActive(false);
        m_time_manager = Time_manager.instance;

        if (PlayerPrefs.GetInt("RewardAfterTimeReady") == 1)
        {
            Reward_gold_after_login();
            Calculate_time_as_gold();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("RewardAfterTimeReady") == 1) 
            Show_text();
    }

    // 메뉴 버튼들을 비활성화 해주는 함수
    void Menu_button_activation(bool _closed)
    {
        if (!_closed)
        {
            for (int i = 0; i < menu_button.Length; i++)
            {
                menu_button[i].enabled = false;
                move_to_next_map_button.enabled = false;
            }
            ms_seller.SetActive(false);
        }
        else
        {
            for (int i = 0; i < menu_button.Length; i++)
            {
                menu_button[i].enabled = true;
                move_to_next_map_button.enabled = true;
            }
            ms_seller.SetActive(true);
        }
    }

    // 텍스트를 띄워주는 함수
    void Show_text()
    {
        string translation_welcome_back_text = UI_translation.Translate_welcome_back_text();
        string translation_reward_x2         = UI_translation.Translate_welcome_back_rewardX2_text();
        welcome_back_money_text.text         = m_time_manager.Time_passed_login(m_time_manager.gold_reward_time);
        welcome_back_text.text               = translation_welcome_back_text;
        reward_x2_text.text                  = translation_reward_x2 + " X2";
    }

    // 재접 후 보상으로 받는 골드 및 부스터 계산
    public void Calculate_time_as_gold()
    {
        m_time_manager.Get_sum_of_time_reward();
        string load_resident_menu_data     = File.ReadAllText(Application.persistentDataPath + "/resident_menu_data.json");
        m_resident_data                    = Json_helper.FromJson<Resident_menu_data>(load_resident_menu_data);

        string load_automation_menu_data   = File.ReadAllText(Application.persistentDataPath + "/automation_menu_data.json");
        m_automation_data                  = Json_helper.FromJson<Automation_menu_data>(load_automation_menu_data);

        string load_building_menu_data     = File.ReadAllText(Application.persistentDataPath + "/building_menu_data.json");
        m_building_data                    = Json_helper.FromJson<Building_menu_data>(load_building_menu_data);

        m_sum_of_all_per_sec                 = m_resident_data[0].resident_gold_per_sec + m_automation_data[0].automation_gold_per_sec+m_building_data[0].building_gold_per_sec;
        m_result_reward_after_time           = m_sum_of_all_per_sec * m_time_manager.sum_of_reward_time;
        reward_gold_text.text              = Large_number.ToString(Math.Truncate(m_result_reward_after_time)).ToString();
        Menu_button_activation(false);
    }

    // 재접속 하면 골드를 주는 함수
    public void Reward_gold_after_login()
    {
        login_gold_object.SetActive(true);
        daily_reward_object.SetActive(false);
        Time.timeScale = 0f;
    }

    // 재접속 골드 창 닫아줌
    public void Close_reward_gold_after_login()
    {
        login_gold_object.SetActive(false);
        Time.timeScale = 1f;

        if (PlayerPrefs.GetInt("Booster_on") == 1)
        {
            float value_as_time = Convert.ToInt32(m_time_manager.sum_of_reward_time);

            if (m_time_manager.current_boostValue_gameplay > 0f) 
                m_time_manager.current_boostValue_gameplay -= value_as_time;

            Data_controller.instance.gold += (m_result_reward_after_time*1.75f);
        }
        else 
            Data_controller.instance.gold += m_result_reward_after_time;

        Menu_button_activation(true);
        daily_reward_object.SetActive(true);
        Audio_manager.instance.Play_touch_sound();
    }

    // 재접속 x2 보상 창 닫아줌
    public void Close_reward_gold_watching_ad()
    {
        for (int i = 0; i < menu_button.Length; i++)
        {
            menu_button[i].enabled = true;
        }
        login_gold_object.SetActive(false);
        Time.timeScale = 1f;

        if (PlayerPrefs.GetInt("Booster_on") == 1)
        {
            float value_as_time = Convert.ToInt32(m_time_manager.sum_of_reward_time);

            if (m_time_manager.current_boostValue_gameplay > 0f) 
                m_time_manager.current_boostValue_gameplay -= value_as_time;

            Data_controller.instance.gold += (m_result_reward_after_time * 3.75f);
        }
        else 
            Data_controller.instance.gold += (m_result_reward_after_time*2);

        Menu_button_activation(true);
        daily_reward_object.SetActive(true);
        Audio_manager.instance.Play_touch_sound();
    }
}