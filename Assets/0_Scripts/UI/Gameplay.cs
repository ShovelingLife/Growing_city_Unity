using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;

// 싱글톤 생성, 게임내 볼륨 제어, 최대 소지할 수 있는 동전, 텍스트, 캐시값, 부스터값, 화면 터치할 때마다 동전 생성, 화면 흔들림 및 사운드.
public class Gameplay : MonoBehaviour
{
    // 게임 관련
    public Auto_scroll auto_scroll;
    public Screen_shake shaker;
    public float shakeDuration = 1f;
    // UI 관련
    public GameObject object_maxMoney;
    public Text txt_currentCash;
    public Text txt_currentCoin;
    public Text txt_currentBoost;
    public Text txt_max_money;

    private void Awake()
    {
        object_maxMoney.SetActive(false);
    }

    void Start()
    {
        Init_gameplay();
    }

    //메인 게임 터치 이벤트, 최대 보유 할 수 있는 동전 = 1e36
    void Update()
    {
        Show_current_text_UI();
    }

    private void OnEnable()
    {
        StartCoroutine(Add_gold_loop());
    }

    // Gameplay 초기화
    void Init_gameplay()
    {
        if (Data_controller.instance.gold == 0) Data_controller.instance.gold += 1.5f;
        txt_currentCoin.text = Data_controller.instance.gold.ToString();
    }

    // 현재 보유하고 있는 화폐 출력
    void Show_current_text_UI()
    {
        txt_currentBoost.text = Time_manager.instance.Gameplay_run_booster();
        txt_currentCash.text = Data_controller.instance.cash.ToString();
        txt_currentCoin.text = Large_number.ToString(Data_controller.instance.gold).ToString();
    }

    // 게임 플레이
    public void On_play()
    {
         if (!PlayerPrefs.HasKey("FirstTime"))
        {
            if (Tutorial_manager.step < 8) return;
            if (Tutorial_manager.step == 8) StartCoroutine("IE_one_time_tutorial");
        }
        Tutorial_manager.step = 9;
        auto_scroll.Close_all_menu(true);
        if (Data_controller.instance.gold < 1e36)
        {
            shaker.Shake(shakeDuration);
            Data_controller.instance.gold += Data_controller.instance.gold_per_click_gold;
            Data_controller.instance.gold += Data_controller.instance.gold_per_click_resident;
            Object_pooling_manager.instance.GetQueue_Coin();
            Audio_manager.instance.Play_coin_sound();
        }
        // 골드 보유 가능한 액수 초과시 최대치 코루틴 발생
        else if (Data_controller.instance.gold >= 1e36)
        {
            txt_max_money.text = Max_money_available_text_translation();
            StartCoroutine(Max_money_available());
            return;
        }
    }

    // 최대 보유 가능한 코인 초과 메시지 코루틴.
    IEnumerator Max_money_available()
    {
        object_maxMoney.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        object_maxMoney.SetActive(false);
    }

    string Max_money_available_text_translation()
    {
        if (Lean.Localization.LeanLocalization.CurrentLanguage == "Spanish") return "Limite de dinero.";
        if (Lean.Localization.LeanLocalization.CurrentLanguage == "English") return "Limit of money.";
        if (Lean.Localization.LeanLocalization.CurrentLanguage == "Korean") return "보유 가능한 돈 초과.";
        else return null;
    }

    // 초당 값이 이상하게 올라가서 6만큼 나눔, Math.Log랑 관련 있는거같음
    IEnumerator Add_gold_loop()
    {
        while (Data_controller.instance.gold <= 1e36)
        {
            Data_controller.instance.gold += Data_controller.instance.gold_per_sec_resident;
            Data_controller.instance.gold += Data_controller.instance.gold_per_sec_automation;
            Data_controller.instance.gold += Data_controller.instance.gold_per_sec_building;
            if (PlayerPrefs.GetInt("Booster_on") == 1) yield return new WaitForSeconds(0.5f);
            else yield return new WaitForSeconds(1.0f);
        }
    }
}