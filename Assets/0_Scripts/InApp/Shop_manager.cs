using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop_manager : Singleton_local<Shop_manager>
{
    // 상점 관련
    public GameObject       shop_menu;
    public Pause_menu       option_menu;

    // 캐쉬 관련
    Dictionary<string, int> m_list_cash;
    int                     m_cash_value;

    // 동전 관련
    Dictionary<string, int> m_list_gold;
    Dictionary<string, int> m_list_gold_value;

    // 아이템 관련
    Dictionary<string, int> m_list_item;


    private void Start()
    {
        Init_dictionary();
        shop_menu.SetActive(false);
    }

    // 아이템 딕셔너리 초기화
    void Init_dictionary()
    {
        m_list_cash = new Dictionary<string, int>()
        {
            {"small_amount_of_cash" ,50  },
            {"medium_amount_of_cash",125 },
            {"bag_of_diamond"       ,250 },
            {"chest_of_diamond"     ,500 }
        };
        m_list_gold = new Dictionary<string, int>()
        {
            { "small_amount_of_gold" ,25  },
            { "medium_amount_of_gold",60  },
            { "bag_of_gold"          ,120 },
            { "chest_of_gold"        ,240 }
        };
        m_list_gold_value = new Dictionary<string, int>()
        {
            { "small_amount_of_gold" ,250000  },
            { "medium_amount_of_gold",600000  },
            { "bag_of_gold"          ,1300000 },
            { "chest_of_gold"        ,2800000 }
        };
        m_list_item = new Dictionary<string, int>()
        {
            { "no_ad"                  ,10 },
            { "booster_ad"             ,5  },
            { "booster_fifteen_minutes",15 },
            { "booster_thirty_minutes" ,30 }
        };
    }

    // 샵 메뉴 열음.
    public void Open_shop_menu()
    {
        shop_menu.SetActive(true);
        Audio_manager.instance.Play_touch_sound();
    }

    // 샵 메뉴 닫음
    public void Exit_shop_menu()
    {
        shop_menu.SetActive(false);
        option_menu.Hide_all_buttons();
        Audio_manager.instance.Play_touch_sound();
        Shop_alert_manager.instance.Turn_off();
    }

    // 작은 크기의 동전 주머니 구매
    public void Add_small_amount_gold()
    {
        Buy_gold("small_amount_of_gold");
    }

    // 중간 크기의 동전 주머니 구매
    public void Add_medium_amount_gold()
    {
        Buy_gold("medium_amount_of_gold");
    }

    // 큰 크기의 동전 주머니 구매
    public void Add_bag_of_gold()
    {
        Buy_gold("bag_of_gold");
    }

    // 돈이 가득한 상자 구매
    public void Add_chest_of_gold()
    {
        Buy_gold("chest_of_gold");
    }

    // 15분짜리 부스트 구매
    public void Buy_booster_fifteen()
    {
        Buy_booster("booster_fifteen_minutes");
    }

    // 30분짜리 부스트 구매
    public void Buy_booster_thirty()
    {
        Buy_booster("booster_thirty_minutes");
    }

    // 골드를 구매할 수 있는지 표시해주는 함수
    void Buy_gold(string product_type)
    {
        Audio_manager.instance.Play_touch_sound();
        m_cash_value = m_list_gold[product_type];

        if (Data_controller.instance.cash > m_cash_value)
        {
            Shop_alert_manager.instance.Run_thanks_for_buying_alert();
            Data_controller.instance.cash -= m_cash_value;
            double gold = 0;

            switch (product_type) // 아이템 종류별로 구매
            {
                case "small_amount_of_gold":  gold = 250000;  break;
                case "medium_amount_of_gold": gold = 600000;  break;
                case "bag_of_gold":           gold = 1300000; break;
                case "chest_of_gold":         gold = 2800000; break;
            }
            Data_controller.instance.gold += gold;
        }
        else 
            Shop_alert_manager.instance.Run_not_enough_cash_alert();
    }

    // 부스터를 구매할 수 있는지 표시해주는 함수
    void Buy_booster(string product_type)
    {
        Audio_manager.instance.Play_touch_sound();
        m_cash_value = m_list_item[product_type];

        if (Data_controller.instance.cash > m_cash_value)
        {
            Shop_alert_manager.instance.Run_thanks_for_buying_alert();
            Data_controller.instance.cash -= m_cash_value;

            switch (product_type) // 아이템 종류별로 구매
            {
                case "booster_fifteen_minutes": Time_manager.instance.Set_gameplay_booster_values(false, 900f);  break;
                case "booster_thirty_minutes":  Time_manager.instance.Set_gameplay_booster_values(false, 1800f); break;
            }
        }
        else 
            Shop_alert_manager.instance.Run_not_enough_cash_alert();
    }

    // 게임의 캐시를 추가해주는 함수
    public void Add_cash(int cashBought)
    {
        Audio_manager.instance.Play_touch_sound();
        Shop_alert_manager.instance.Run_thanks_for_buying_alert();
        Data_controller.instance.cash += cashBought;
    }
}