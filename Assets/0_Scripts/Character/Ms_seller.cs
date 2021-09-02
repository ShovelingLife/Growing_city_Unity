using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Ms_seller : MonoBehaviour
{
    // 상인 아주머니 관련
    public GameObject ms_seller_text_object;
    public Text ms_seller_text;
    Rigidbody2D m_rigidbody;
    float jump_power = 2f;
    int jump_count = 0;
    bool is_jumping = false;

    // 일일 보상 상점 관련
    public GameObject object_daily_shop;
    public Text daily_shop_text;
    public Text current_time_text;
    public Text claim_text;
    public Button[] menu_button = new Button[4];
    public Image[] diamond_image = new Image[8];
    public Sprite received_image;
    public Sprite reset_image;
    bool received = false;

    // 시간 관련
    Timer_settings m_timer_settings;
    DateTime time;
    TimeSpan time_span;
    double   total_time = 0f;
    int      day = 0;

    // 스크롤
    public Upgrade_menu_core auto_scroll;


    private void Awake()
    {
        Set_time();
    }

    void Start()
    {
        // 보상 수령 했으면 꺼두기
        if (received) 
            ms_seller_text_object.SetActive(false);

        if (day!=0) 
            Change_image();

        m_timer_settings = GetComponent<Timer_settings>();
        m_rigidbody      = GetComponent<Rigidbody2D>();
        object_daily_shop.SetActive(false);
    }

    private void Update()
    {
        Show_current_time();

        if (day == 8) 
            Reset_images();
    }

    // 터치 이벤트
    private void OnMouseUpAsButton()
    {
        Show_daily_reward();
        auto_scroll.Close_all_menu(true);
    }

    // 상점 아주머니 이벤트
    IEnumerator IE_Ms_seller_talks()
    {
        string translation_ms_seller_text = UI_translation.Translate_ms_seller_text();
        ms_seller_text_object.SetActive(true);
        ms_seller_text.text = translation_ms_seller_text;
        yield return new WaitForSeconds(1.5f);
        ms_seller_text_object.SetActive(false);
    }

    // 상점 아주머니가 말함
    public void Ms_seller_talks(bool talk_ready)
    {
        if (!talk_ready)
        {
            StopCoroutine("IE_Ms_seller_talks");
            return;
        }
        else StartCoroutine("IE_Ms_seller_talks");
    }

    // 시간을 설정하는 함수
    void Set_time()
    {
        if (!PlayerPrefs.HasKey("NewTime"))
        {
            time = DateTime.UtcNow;
            time = time.AddDays(1);
        }
        else
        {
            time = DateTime.Parse(PlayerPrefs.GetString("NewTime"));
        }
        time_span = time - DateTime.UtcNow;
        total_time = time_span.TotalSeconds;
        day = PlayerPrefs.GetInt("Reward_day");
        received = Convert.ToBoolean(PlayerPrefs.GetInt("Received"));
    }

    private void FixedUpdate()
    {
        if (!received)
        {
            Ms_seller_jump();
            Ms_seller_recovery();
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Received", Convert.ToInt32(received));
        PlayerPrefs.SetString("NewTime", time.ToString());
        PlayerPrefs.Save();
    }

    // 이미지를 변경해주는 함수
    void Change_image()
    {
        for (int i = 0; i < day; i++)
             diamond_image[i].sprite = received_image;
    }

    // 이미지를 초기화해주는 함수
    void Reset_images()
    {
        for (int i = 0; i < diamond_image.Length; i++)
             diamond_image[i].sprite = reset_image;
    }

    // 충돌체크
    private void OnCollisionEnter2D(Collision2D ms_seller)
    {
        if (ms_seller.gameObject.tag == "Ms_seller_collider") 
            is_jumping = false;
    }

    // NPC가 점프하는 함수
    void Ms_seller_jump()
    {
        if (is_jumping) 
            return;

        if (jump_count > 3)
        {
            Ms_seller_talks(false);
            return;
        }
        m_rigidbody.AddForce(Vector2.up * jump_power, ForceMode2D.Impulse);
        Ms_seller_talks(true);
        is_jumping = true;
        jump_count++;
    }

    // NPC 재활성
    void Ms_seller_recovery()
    {
        if (m_timer_settings.Is_timer_finished())
            jump_count = 0;
    }

    // 현재 시간 띄워주는 함수
    void Show_current_time()
    {
        string translation_claim_text = UI_translation.Translate_claim_reward_text();
        current_time_text.text        = Time_passed_daily_reward();
        claim_text.text               = translation_claim_text;
    }

    // 경과한 시간을 반환함 일일보상
    string Time_passed_daily_reward()
    {
        total_time -= Time.deltaTime;
        time_span  = TimeSpan.FromSeconds(total_time);

        if (total_time < 0f)
        {
            received   = false;
            time       = DateTime.Now;
            time       = time.AddDays(1);
            time_span  = time - DateTime.Now;
            total_time = time_span.TotalSeconds;
        }
        return string.Format("{0:00}:{1:00}:{2:00}:{3:00}", time_span.Days, time_span.Hours, time_span.Minutes, time_span.Seconds);
    }

    // 일일 상점을 보여주는 함수
    void Show_daily_reward()
    {
        string translation_daily_shop_text = UI_translation.Translate_daily_reward_shop_text();
        daily_shop_text.text               = translation_daily_shop_text;
        object_daily_shop.SetActive(true);
    }

    // 일일 상점을 닫아주는 함수
    public void Close_daily_reward()
    {
        object_daily_shop.SetActive(false);
        Audio_manager.instance.Play_touch_sound();
        Daily_reward_quantity();
        Change_image();
    }

    // 일일보상 몇개 획득하는지 판별해주는 함수
    void Daily_reward_quantity()
    {
        if (received) 
            return;

        if (day == 8) 
            day = 0;

        switch (day)
        {
            case 0: Data_controller.instance.cash += 5;   break;
                                                          
            case 1: Data_controller.instance.cash += 10;  break;
                                                          
            case 2: Data_controller.instance.cash += 20;  break;
                                                          
            case 3: Data_controller.instance.cash += 35;  break;
                                                          
            case 4: Data_controller.instance.cash += 50;  break;
                                                          
            case 5: Data_controller.instance.cash += 65;  break;

            case 6: Data_controller.instance.cash += 80;  break;

            case 7: Data_controller.instance.cash += 100; break;
        }
        day++;
        PlayerPrefs.SetInt("Reward_day", day);
        received = true;
    }

    // 메뉴 버튼들을 비활성화 해주는 함수
    void Menu_button_activation(bool _closed)
    {
        if (_closed)
        {
            for (int i = 0; i < menu_button.Length; i++)
                 menu_button[i].enabled = false;
        }
        else
        {
            for (int i = 0; i < menu_button.Length; i++)
                 menu_button[i].enabled = true;
        }
    }
}