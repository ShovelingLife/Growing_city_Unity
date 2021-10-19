using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


// 업그레이드 버튼 클래스

[Serializable]
public class Upgrade_button_storage
{
    public Upgrade_button_crop[]       arr_crop_upgrade_button       = null;
    public Upgrade_button_city[]       arr_city_upgrade_button       = null;
    public Upgrade_button_resident[]   arr_resident_upgrade_button   = null;
    public Upgrade_button_automation[] arr_automation_upgrade_button = null;
    public int                         total_crop_upgrade            = 0;
    public int                         total_city_upgrade            = 0;
    public int                         total_resident_upgrade        = 0;
    public int                         total_automation_upgrade      = 0;
}

// 최상위 자원들을 관리
public class Upgrade_menu_manager : Singleton_local<Upgrade_menu_manager>
{
    // 각 메뉴 버튼들 및 스크롤
    Scroll_menu[]                  m_arr_menu        = null;
    public Button[]                arr_menu_button = null;
    Dictionary<string, GameObject> m_dic_menu_obj = new Dictionary<string, GameObject>();
    bool m_is_opened = false;
    
    // 전체 초당 배열
    Upgrade_property_core[]       m_arr_per_sec_core;

    // 각 업그레이드 버튼들
    public Upgrade_button_storage upgrade_button_storage;


    void Awake()
    {
        Init();
        Init_buttons();
        Close_all_menu();
    }

    // 초기화
    void Init()
    {
        m_arr_menu         = GameObject.FindObjectsOfType<Scroll_menu>();
        m_arr_per_sec_core = GameObject.FindObjectsOfType<Upgrade_property_core>();
        int menu_size      = m_arr_menu.Length;
        arr_menu_button  = new Button[menu_size];
        
        // 버튼 추가
        for (int i = 0; i < menu_size; i++)
        {
            arr_menu_button[i] = m_arr_menu[i].transform.GetChild(0).gameObject.GetComponent<Button>();
            arr_menu_button[i].onClick.AddListener(Check_scrolling_direction);
            m_arr_menu[i].Init_obj();
            m_dic_menu_obj.Add(arr_menu_button[i].name, m_arr_menu[i].scroll_menu_obj);
        }
    }

    // 버튼 초기화
    void Init_buttons()
    {
        // 전체 업그레이드 버튼들을 가져옴
        upgrade_button_storage.arr_crop_upgrade_button       = Resources.FindObjectsOfTypeAll<Upgrade_button_crop>();
        upgrade_button_storage.arr_city_upgrade_button       = Resources.FindObjectsOfTypeAll<Upgrade_button_city>();
        upgrade_button_storage.arr_resident_upgrade_button   = Resources.FindObjectsOfTypeAll<Upgrade_button_resident>();
        upgrade_button_storage.arr_automation_upgrade_button = Resources.FindObjectsOfTypeAll<Upgrade_button_automation>();

        // 크기를 지정
        upgrade_button_storage.total_crop_upgrade       = upgrade_button_storage.arr_crop_upgrade_button.Length;
        upgrade_button_storage.total_city_upgrade       = upgrade_button_storage.arr_city_upgrade_button.Length;
        upgrade_button_storage.total_resident_upgrade   = upgrade_button_storage.arr_resident_upgrade_button.Length;
        upgrade_button_storage.total_automation_upgrade = upgrade_button_storage.arr_automation_upgrade_button.Length;

        // 이름 순으로 정렬
        upgrade_button_storage.arr_crop_upgrade_button.OrderBy(x => x.id);
        upgrade_button_storage.arr_city_upgrade_button.OrderBy(x => x.id);        
        upgrade_button_storage.arr_resident_upgrade_button.OrderBy(x => x.id);
        upgrade_button_storage.arr_automation_upgrade_button.OrderBy(x => x.id);
    }


    // 내릴지 올릴지 결정
    public void Check_scrolling_direction()
    {
        if(m_is_opened) // 단 하나라도 열려있다면 전체 메뉴 숨기
        {
            foreach (var item in m_dic_menu_obj)
                     item.Value.SetActive(false);
        }
        else
        {
            foreach (var item in m_arr_menu)
            {
                item.Scrolling_menu_up();
                item.scroll_menu_obj.SetActive(false);
            }
        }
        string name = EventSystem.current.currentSelectedGameObject.name;
        m_dic_menu_obj[EventSystem.current.currentSelectedGameObject.name].SetActive(true);
    }

    // 모든 메뉴를 열음
    public void Open_all_menu()
    {
        foreach (var item in m_arr_menu)
                 item.Scrolling_menu_up();
    }

    // 모든 메뉴를 닫음
    public void Close_all_menu()
    {
        foreach (var item in m_arr_menu)
                 item.Scrolling_menu_down();

        m_is_opened = false;
    }

    // 모든 메뉴 버튼들을 비활성화
    public void Deactivate_all_menu_buttons()
    {
        foreach (var item in arr_menu_button)
                 item.gameObject.SetActive(false);
    }

    // 모든 메뉴 버튼들을 활성화
    public void Activate_all_menu_buttons()
    {
        foreach (var item in arr_menu_button)
                 item.gameObject.SetActive(true);
    }

    // 스크롤 원위치 시킴
    public void Set_scroll_at_original_pos(int _index)
    {
        // Rect를 갖고와서 원위치
        RectTransform rect           = m_arr_per_sec_core[_index].GetComponent<RectTransform>();
        Vector3 new_pos              = rect.transform.localPosition;
        new_pos.y                    = -945f;
        rect.transform.localPosition = new_pos;
    }

    // 전 작물 업그레이드 비용들에 값을 곱해줌 1
    public void Apply_multiplier1_on_crop_upgrade_menu()
    {
        foreach (var item in upgrade_button_storage.arr_crop_upgrade_button)
                 item.upgrade_multiplier_prop = 1;
    }
    
    // 전 작물 업그레이드 비용들에 값을 곱해줌 10
    public void Apply_multiplier10_on_crop_upgrade_menu()
    {
        foreach (var item in upgrade_button_storage.arr_crop_upgrade_button)
                 item.upgrade_multiplier_prop = 10;
    }

    // 전 작물 업그레이드 비용들에 값을 곱해줌 50
    public void Apply_multiplier50_on_crop_upgrade_menu()
    {
        foreach (var item in upgrade_button_storage.arr_crop_upgrade_button)
                 item.upgrade_multiplier_prop = 50;
    }

    // 전 도시 업그레이드 비용들에 값을 곱해줌 1
    public void Apply_multiplier1_on_city_upgrade_menu()
    {
        foreach (var item in upgrade_button_storage.arr_city_upgrade_button)
            item.upgrade_multiplier_prop = 1;
    }

    // 전 도시 업그레이드 비용들에 값을 곱해줌 10
    public void Apply_multiplier10_on_city_upgrade_menu()
    {
        foreach (var item in upgrade_button_storage.arr_city_upgrade_button)
            item.upgrade_multiplier_prop = 10;
    }

    // 전 도시 업그레이드 비용들에 값을 곱해줌 50
    public void Apply_multiplier50_on_city_upgrade_menu()
    {
        foreach (var item in upgrade_button_storage.arr_city_upgrade_button)
            item.upgrade_multiplier_prop = 50;
    }

    // 전 시민 업그레이드 비용들에 값을 곱해줌 1
    public void Apply_multiplier1_on_resident_upgrade_menu()
    {
        foreach (var item in upgrade_button_storage.arr_resident_upgrade_button)
            item.upgrade_multiplier_prop = 1;
    }

    // 전 시민 업그레이드 비용들에 값을 곱해줌 10
    public void Apply_multiplier10_on_resident_upgrade_menu()
    {
        foreach (var item in upgrade_button_storage.arr_resident_upgrade_button)
            item.upgrade_multiplier_prop = 10;
    }

    // 전 시민 업그레이드 비용들에 값을 곱해줌 50
    public void Apply_multiplier50_on_resident_upgrade_menu()
    {
        foreach (var item in upgrade_button_storage.arr_resident_upgrade_button)
            item.upgrade_multiplier_prop = 50;
    }

    // 전 기계화 업그레이드 비용들에 값을 곱해줌 1
    public void Apply_multiplier1_on_automation_upgrade_menu()
    {
        foreach (var item in upgrade_button_storage.arr_automation_upgrade_button)
            item.upgrade_multiplier_prop = 1;
    }

    // 전 기계화 업그레이드 비용들에 값을 곱해줌 10
    public void Apply_multiplier10_on_automation_upgrade_menu()
    {
        foreach (var item in upgrade_button_storage.arr_automation_upgrade_button)
            item.upgrade_multiplier_prop = 10;
    }

    // 전 기계화 업그레이드 비용들에 값을 곱해줌 50
    public void Apply_multiplier50_on_automation_upgrade_menu()
    {
        foreach (var item in upgrade_button_storage.arr_automation_upgrade_button)
            item.upgrade_multiplier_prop = 50;
    }
}