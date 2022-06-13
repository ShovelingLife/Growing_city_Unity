using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Ms_seller : MonoBehaviour
{
    // 상인 아주머니 관련
    public GameObject     ms_seller_text_object;
    public Text           ms_seller_text;
           float          m_jump_power = 2f;
           int            m_jump_count = 0;
           bool           m_is_jumping = false;

    // 일일 보상 상점 관련
    public GameObject     object_daily_shop;
    public Text           txt_daily_shop;
    public Text           txt_current_time;
    public Text           txt_claim;
    [SerializeField]
           Image[]        m_arr_daily_reward_item;
    public Transform      daily_reward_parent;
    public Sprite         received_image;
    public Sprite         reset_image;
           bool           m_received = false;

    // 시간 관련
    public Timer_settings     timer_settings;
           DateTime           m_time;
           TimeSpan           m_time_span;
           double             m_total_time = 0f;
           int                m_day = 0;
           Csv_loader_manager inst;


    private void Awake()
    {
        Set_time();
    }

    void Start()
    {
        inst = Csv_loader_manager.instance;
        // 일일 보상 아이템 초기화
        m_arr_daily_reward_item = new Image[daily_reward_parent.childCount];

        for (int i = 0; i < m_arr_daily_reward_item.Length; i++)
             m_arr_daily_reward_item[i] = daily_reward_parent.GetChild(i).gameObject.GetComponent<Image>();

        // 보상 수령 했으면 꺼두기
        if (m_received) 
            ms_seller_text_object.SetActive(false);

        if (m_day!=0) 
            Change_image();

        object_daily_shop.SetActive(false);
    }

    private void Update()
    {
        Show_text();

        if (m_day == 8) 
            Reset_images();
    }

    // 터치 이벤트
    private void OnMouseUpAsButton()
    {
        Show_daily_reward();
        Upgrade_menu_manager.instance.Close_all_menu();
    }

    // 상점 아주머니 이벤트
    IEnumerator IE_Ms_seller_talks()
    {
        ms_seller_text.text = inst[inst.Get_hash_code_by_str("DAILY_REWARD_AVAILABLE")].ToString();
        ms_seller_text_object.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        ms_seller_text_object.SetActive(false);
    }

    // 시간을 설정하는 함수
    void Set_time()
    {
        if (!PlayerPrefs.HasKey("NewTime"))
        {
            m_time = DateTime.UtcNow;
            m_time = m_time.AddDays(1);
        }
        else
            m_time = DateTime.Parse(PlayerPrefs.GetString("NewTime"));

        m_time_span  = m_time - DateTime.UtcNow;
        m_total_time = m_time_span.TotalSeconds;
        m_day        = PlayerPrefs.GetInt("Reward_day");
        m_received     = Convert.ToBoolean(PlayerPrefs.GetInt("Received"));
    }

    private void FixedUpdate()
    {
        if (!m_received)
        {
            Ms_seller_jump();
            Ms_seller_recovery();
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Received", Convert.ToInt32(m_received));
        PlayerPrefs.SetString("NewTime", m_time.ToString());
        PlayerPrefs.Save();
    }

    // 이미지를 변경해주는 함수
    void Change_image()
    {
        for (int i = 0; i < m_day; i++)
        {
            // X 표시 후 다이아몬드 및 값 제거
            m_arr_daily_reward_item[i].sprite = received_image;
            m_arr_daily_reward_item[i].transform.GetChild(0).gameObject.SetActive(false);
            m_arr_daily_reward_item[i].transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    // 이미지를 초기화해주는 함수
    void Reset_images()
    {
        for (int i = 0; i < m_arr_daily_reward_item.Length; i++)
        {
            // 다이아몬드 및 값 표시
            m_arr_daily_reward_item[i].sprite = reset_image;
            m_arr_daily_reward_item[i].transform.GetChild(0).gameObject.SetActive(true);
            m_arr_daily_reward_item[i].transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    // 충돌체크
    private void OnCollisionEnter2D(Collision2D ms_seller)
    {
        if (ms_seller.gameObject.tag == "Ms_seller_collider") 
            m_is_jumping = false;
    }

    // NPC가 점프하는 함수
    void Ms_seller_jump()
    {
        if (m_is_jumping) 
            return;

        if (m_jump_count > 3)
        {
            Ms_seller_talks(false);
            return;
        }
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * m_jump_power, ForceMode2D.Impulse);
        Ms_seller_talks(true);
        m_is_jumping = true;
        m_jump_count++;
    }

    // NPC 재활성
    void Ms_seller_recovery()
    {
        if (timer_settings.Is_timer_finished())
            m_jump_count = 0;
    }

    // 현재 시간 띄워주는 함수
    void Show_text()
    {
        txt_daily_shop.text     = inst[inst.Get_hash_code_by_str("DAILY_REWARD")].ToString();
        txt_claim.text          = inst[inst.Get_hash_code_by_str("CLAIM")].ToString();
        txt_current_time.text   = Time_passed_daily_reward();
    }

    // 경과한 시간을 반환함 일일보상
    string Time_passed_daily_reward()
    {
        m_total_time -= Time.deltaTime;
        m_time_span  = TimeSpan.FromSeconds(m_total_time);

        if (m_total_time < 0f)
        {
            m_received   = false;
            m_time       = DateTime.Now;
            m_time       = m_time.AddDays(1);
            m_time_span  = m_time - DateTime.Now;
            m_total_time = m_time_span.TotalSeconds;
        }
        return string.Format("{0:00}:{1:00}:{2:00}:{3:00}", m_time_span.Days, m_time_span.Hours, m_time_span.Minutes, m_time_span.Seconds);
    }

    // 일일 상점을 보여주는 함수
    void Show_daily_reward()
    {
        //string translation_daily_shop_text = UI_translation.Translate_daily_reward_shop_text();
        //daily_shop_text.text               = translation_daily_shop_text;
        object_daily_shop.SetActive(true);
    }

    // 일일보상 몇개 획득하는지 판별해주는 함수
    void Daily_reward_quantity()
    {
        if (m_received) 
            return;

        if (m_day == 8) 
            m_day = 0;

        int[] arr_cash_val = { 5, 10, 20, 35, 50, 65, 80, 100 };
        Data_controller.instance.cash += arr_cash_val[m_day++];
        PlayerPrefs.SetInt("Reward_day", m_day);
        m_received = true;
    }

    // 메뉴 버튼들을 비활성화 해주는 함수
    void Menu_button_activation(bool _closed)
    {
        if (_closed)
            Upgrade_menu_manager.instance.Deactivate_all_menu_buttons();

        else
            Upgrade_menu_manager.instance.Activate_all_menu_buttons();
    }

    // 일일 상점을 닫아주는 함수
    public void Close_daily_reward()
    {
        object_daily_shop.SetActive(false);
        //Audio_manager.instance.Play_touch_sound();
        Daily_reward_quantity();
        Change_image();
    }

    // 상점 아주머니가 말함
    public void Ms_seller_talks(bool talk_ready)
    {
        if (!talk_ready)
            StopCoroutine("IE_Ms_seller_talks");

        else
            StartCoroutine("IE_Ms_seller_talks");
    }
}