using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class Auto_scroll : MonoBehaviour
{
    public Text             crop_menu_text;
    public Text             city_menu_text;
    public Text             mechanization_menu_text;
    public Text             resident_menu_text;
    public RectTransform    crop_menu, city_menu, automation_menu, resident_menu;
    public RectTransform    crop_upgrade_menu, city_upgrade_menu, automation_upgrade_menu, resident_upgrade_menu;
    public ScrollRect       crop_menu_scroll, city_menu_scroll, automation_menu_scroll, resident_menu_scroll;
    public Animator         crop_upgrade_button, city_upgrade_button, automation_upgrade_button, resident_upgrade_button;
    bool                    is_crop_menu_opened, is_city_menu_opened, is_automation_menu_opened, is_resident_menu_opened;


    private void Start()
    {
        // 애니메이션 재생을 위해 Animator GetComponent
        Close_all_menu();
        crop_upgrade_button.gameObject.GetComponent<Animator>();
        city_upgrade_button.gameObject.GetComponent<Animator>();
        automation_upgrade_button.gameObject.GetComponent<Animator>();
        resident_upgrade_button.gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        Menu_translation();
    }

    // 메뉴명을 번역해주는 함수
    public void Menu_translation()
    {
        crop_menu_text.text          = UI_translation.Translate_crop_menu();
        city_menu_text.text          = UI_translation.Translate_city_menu();
        mechanization_menu_text.text = UI_translation.Translate_mechanization_menu();
        resident_menu_text.text      = UI_translation.Translate_resident_menu();
    }

    // Scrolling up all menu
    void Scroll_up_all_upgrade_menu()
    {
        crop_menu.DOAnchorPosY(-705, 0);
        city_menu.DOAnchorPosY(-705, 0);
        automation_menu.DOAnchorPosY(-705, 0);
        resident_menu.DOAnchorPosY(-705, 0);
    }

    public void Open_and_close_crop_menu() // cropMenu를 열어주거나 닫아줌.
    {
        Tutorial_manager.step = 7;
        PlayerPrefs.SetInt("isSwaping", 0);
        Turn_on_crop_button();
        Audio_manager.instance.Play_touch_sound();

        if (!is_crop_menu_opened)
        {
            Scroll_up_all_upgrade_menu();
            Swap_to_crop_menu();
            is_crop_menu_opened = true;
        }
        else
        {
            Swap_to_crop_menu();
            crop_upgrade_menu.DOAnchorPosY(-945, 0);
            is_crop_menu_opened = false;
        }
    }

    public void Open_and_close_city_menu()  // cityMenu를 열어주거나 닫아줌.
    {
        PlayerPrefs.SetInt("isSwaping", 1);
        Turn_on_city_button();
        Audio_manager.instance.Play_touch_sound();

        if (!is_city_menu_opened)
        {
            Scroll_up_all_upgrade_menu();
            Swap_to_city_menu();
            is_city_menu_opened = true;
        }
        else
        {
            Swap_to_city_menu();
            city_upgrade_menu.DOAnchorPosY(-945, 0);
            is_city_menu_opened = false;
        }
    }

    public void Open_and_close_automation_menu()  // automationMenu를 열어주거나 닫아줌.
    {
        Turn_on_automation_button();
        Audio_manager.instance.Play_touch_sound();

        if (!is_automation_menu_opened)
        {
            Scroll_up_all_upgrade_menu();
            Swap_to_automation_menu();
            is_automation_menu_opened = true;
        }
        else
        {
            automation_upgrade_menu.DOAnchorPosY(-945, 0);
            Swap_to_automation_menu();
            is_automation_menu_opened = false;
        }
    }

    public void Open_and_close_resident_menu()  // residentMenu를 열어주거나 닫아줌.
    {
        Turn_on_resident_button();
        Audio_manager.instance.Play_touch_sound();

        if (!is_resident_menu_opened)
        {
            Scroll_up_all_upgrade_menu();
            Swap_to_resident_menu();
            is_resident_menu_opened = true;
        }
        else
        {
            resident_upgrade_menu.DOAnchorPosY(-945, 0);
            is_resident_menu_opened = false;
            Swap_to_resident_menu();
        }
    }

    void Hide_menu()
    {
        crop_menu_scroll.gameObject.SetActive(false);
        city_menu_scroll.gameObject.SetActive(false);
        automation_menu_scroll.gameObject.SetActive(false);
        resident_menu_scroll.gameObject.SetActive(false);
    }

    void Swap_to_crop_menu() // 타 메뉴에서 cropMenu로 변경.
    {
        Multiply_manager.instance.Reset_button_when_closed();
        Hide_menu();
        crop_menu_scroll.gameObject.SetActive(true);
    }

    void Swap_to_city_menu()  // 타 메뉴에서 cityMenu로 변경.
    {
        Multiply_manager.instance.Reset_button_when_closed();
        Hide_menu();
        city_menu_scroll.gameObject.SetActive(true);
    }

    void Swap_to_automation_menu()  // 타 메뉴에서 automationMenu로 변경.
    {
        Multiply_manager.instance.Reset_button_when_closed();
        Hide_menu();
        automation_menu_scroll.gameObject.SetActive(true);
    }

    void Swap_to_resident_menu()  // 타 메뉴에서 residentMenu로 변경.
    {
        Multiply_manager.instance.Reset_button_when_closed();
        Hide_menu();
        resident_menu_scroll.gameObject.SetActive(true);
    }

    void Close_all_menu()  // 전체 메뉴 닫기.
    {
        crop_menu_scroll.gameObject.SetActive(false);
        city_menu_scroll.gameObject.SetActive(false);
        automation_menu_scroll.gameObject.SetActive(false);
        resident_menu_scroll.gameObject.SetActive(false);
        crop_upgrade_button.SetBool("isSelected", false);
        city_upgrade_button.SetBool("isSelected", false);
        automation_upgrade_button.SetBool("isSelected", false);
        resident_upgrade_button.SetBool("isSelected", false);
        crop_menu.DOAnchorPosY(-1225, 0);
        crop_upgrade_menu.DOAnchorPosY(-945, 0);
        city_menu.DOAnchorPosY(-1225, 0);
        city_upgrade_menu.DOAnchorPosY(-945, 0);
        automation_menu.DOAnchorPosY(-1225, 0);
        automation_upgrade_menu.DOAnchorPosY(-945, 0);
        resident_menu.DOAnchorPosY(-1225, 0);
        resident_upgrade_menu.DOAnchorPosY(-945, 0);        
    }

    void Turn_off_all_buttons()
    {
        crop_upgrade_button.SetBool("isSelected", false);
        city_upgrade_button.SetBool("isSelected", false);
        automation_upgrade_button.SetBool("isSelected", false);
        resident_upgrade_button.SetBool("isSelected", false);
    }

    void Turn_on_crop_button()  // cropMenu 버튼 이펙트 (하늘색)
    {
        Turn_off_all_buttons();
        crop_upgrade_button.SetBool("isSelected", true);
    }

    void Turn_on_city_button()  // cityMenu 버튼 이펙트 (하늘색)
    {
        Turn_off_all_buttons();
        city_upgrade_button.SetBool("isSelected", true);
    }

    void Turn_on_automation_button()  // automationMenu 버튼 이펙트 (노랑색)
    {
        Turn_off_all_buttons();
        automation_upgrade_button.SetBool("isSelected", true);
    }

    void Turn_on_resident_button()  // residentMenu 버튼 이펙트 (노랑색)
    {
        Turn_off_all_buttons();
        resident_upgrade_button.SetBool("isSelected", true);
    }

    public void Close_all_menu(bool screenClick)
    {
        if (screenClick)
        {
            Close_all_menu();
            Multiply_manager.instance.Reset_button_when_closed();
        }
    }
}