using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sub_menu_gameplay : MonoBehaviour
{
    // 메인 UI 관련
    public Camera     UI_camera;
    public Canvas     main_UI_canvas;
    public GameObject cant_purchase_message_object;

    // 부 UI 관련
    public Camera sub_UI_camera;
    public Canvas sub_UI_canvas;
    public Text   txt_current_coin;
    public Text   txt_current_coin_per_sec;
    string        m_translation_per_sec;

    // main_UI 상인 관련
    public Camera main_menu_camera;
    public Canvas ms_seller_canvas;
    // sub_UI 건물 관련


    void Start()
    {
        Init_sub_menu();
    }


    void Update()
    {
        Show_current_coin_text();
    }

    // 두번째 ui를 초기화해주는 함수
    void Init_sub_menu()
    {
        Vector3 new_pos       = sub_UI_camera.transform.position;
        sub_UI_camera.enabled = false;
        this.gameObject.SetActive(false);
    }

    // 현재 돈이랑 초당 얻는 돈을 출력하는 함수
    void Show_current_coin_text()
    {
        m_translation_per_sec           = UI_translation.Translate_perSec_text();
        txt_current_coin.text         = Large_number.ToString(Data_controller.instance.gold).ToString();
        txt_current_coin_per_sec.text = m_translation_per_sec + " " + Large_number.ToString(Data_controller.instance.gold_per_sec_building).ToString();
    }

    // 카메라를 다음 화면으로 움직여주는 함수
    public void Move_camera_to_subMenu()
    {
        Tutorial_manager.step    = 5;
        sub_UI_canvas.enabled    = true;
        main_UI_canvas.enabled   = false;
        ms_seller_canvas.enabled = false;
        main_menu_camera.enabled = false;
        UI_camera.enabled        = false;
        sub_UI_camera.enabled    = true;
        cant_purchase_message_object.transform.SetParent(sub_UI_canvas.transform);
        cant_purchase_message_object.transform.position = sub_UI_canvas.transform.position;
        Audio_manager.instance.Play_touch_sound();
        this.gameObject.SetActive(true);
    }

    // 카메라를 이전 화면으로 움직여주는 함수
    public void Move_camera_to_menu()
    {
        Tutorial_manager.step    = 6;
        sub_UI_canvas.enabled    = false;
        main_UI_canvas.enabled   = true;
        ms_seller_canvas.enabled = true;
        main_menu_camera.enabled = true;
        UI_camera.enabled        = true;
        sub_UI_camera.enabled    = false;
        cant_purchase_message_object.transform.SetParent(main_UI_canvas.transform);
        cant_purchase_message_object.transform.position = main_UI_canvas.transform.position;
        Audio_manager.instance.Play_touch_sound();
    }
}