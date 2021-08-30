using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop_manager : Singleton_local<Shop_manager>
{
    // 상점 관련
    public GameObject       shop_menu;
    public Pause_menu       optionMenu;
    public Text             shop_title_text;

    // 캐쉬 관련
    Dictionary<string, int> m_cash_list;
    public Text[]           cash_text_arr = new Text[4];
    public string[]         cash_type_arr = new string[4];
    int                     m_cash_value;

    // 동전 관련
    Dictionary<string, int> m_gold_list;
    Dictionary<string, int> m_gold_value_list;
    public Text[]           gold_text_arr = new Text[4];
    public string[]         gold_type_arr = new string[4];

    // 아이템 관련
    Dictionary<string, int> m_item_list;
    public Text[]           item_text_arr = new Text[4];
    public string[]         item_type_arr = new string[4];


    private void Start()
    {
        Init_dictionary();
        shop_menu.SetActive(false);
    }
    private void Update()
    {
        Translate_cash_type();
        Translate_gold_type();
        Translate_item_type();
        Translate_shop_title();
    }

    // 아이템 딕셔너리 초기화
    void Init_dictionary()
    {
        m_cash_list = new Dictionary<string, int>()
        {
            {"small_amount_of_cash" ,50  },
            {"medium_amount_of_cash",125 },
            {"bag_of_diamond"       ,250 },
            {"chest_of_diamond"     ,500 }
        };
        m_gold_list = new Dictionary<string, int>()
        {
            { "small_amount_of_gold" ,25  },
            { "medium_amount_of_gold",60  },
            { "bag_of_gold"          ,120 },
            { "chest_of_gold"        ,240 }
        };
        m_gold_value_list = new Dictionary<string, int>()
        {
            { "small_amount_of_gold" ,250000  },
            { "medium_amount_of_gold",600000  },
            { "bag_of_gold"          ,1300000 },
            { "chest_of_gold"        ,2800000 }
        };
        m_item_list = new Dictionary<string, int>()
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
        optionMenu.Hide_all_buttons();
        Shop_alert_manager.instance.Stop_all_coroutines();
        Audio_manager.instance.Play_touch_sound();
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

    // 캐시 종류를 번역해주는 함수
    void Translate_cash_type()
    {
        foreach(var item in m_cash_list)
        {
            for(int i = 0; i < cash_type_arr.Length; i++)
            {
                if (cash_type_arr[i] == item.Key)
                {
                    switch (Lean.Localization.LeanLocalization.CurrentLanguage)
                    {
                        case "English":
                            cash_text_arr[i].text = item.Value.ToString() + " DIAMONDS";
                            break;

                        case "Spanish":
                            cash_text_arr[i].text = item.Value.ToString() + " DIAMANTES";
                            break;

                        case "Korean":
                            cash_text_arr[i].text = "보석" + item.Value.ToString() + "개";
                            break;
                    }
                }
            }
        }
    }

    // 골드 종류를 번역해주는 함수
    void Translate_gold_type()
    {
        foreach (var item in m_gold_value_list)
        {
            for (int i = 0; i < gold_type_arr.Length; i++)
            {
                if (gold_type_arr[i] == item.Key)
                {
                    switch (Lean.Localization.LeanLocalization.CurrentLanguage)
                    {
                        case "English":
                            gold_text_arr[i].text = item.Value.ToString() + " GOLDS";
                            break;

                        case "Spanish":
                            gold_text_arr[i].text = item.Value.ToString() + " OROS";
                            break;

                        case "Korean":
                            gold_text_arr[i].text = "동전" + item.Value.ToString() + "개";
                            break;
                    }
                }
            }
        }
    }

    // 아이템 종류를 번역해주는 함수
    void Translate_item_type()
    {
        foreach (var item in m_item_list)
        {
            for (int i = 0; i < item_type_arr.Length; i++)
            {
                if (item_type_arr[i] == "no_ad")
                {
                    switch (Lean.Localization.LeanLocalization.CurrentLanguage)
                    {
                        case "English":
                            item_text_arr[i].text = "NO ADS";
                            break;

                        case "Spanish":
                            item_text_arr[i].text = "NO PROPAGANDA";
                            break;

                        case "Korean":
                            item_text_arr[i].text = "광고 제거";
                            break;
                    }
                }
                else if (item_type_arr[i] == item.Key)
                {
                    switch (Lean.Localization.LeanLocalization.CurrentLanguage)
                    {
                        case "English":
                            item_text_arr[i].text = "Booster " + item.Value.ToString() + " minutes";
                            break;

                        case "Spanish":
                            item_text_arr[i].text = "Booster " + item.Value.ToString() + " minutos";
                            break;

                        case "Korean":
                            item_text_arr[i].text = "부스터 " + item.Value.ToString() + " 분";
                            break;
                    }
                }
            }
        }
    }

    // 상점 제목을 변역해주는 함수
    void Translate_shop_title()
    {
        switch (Lean.Localization.LeanLocalization.CurrentLanguage)
        {
            case "English":
                shop_title_text.text = "SHOP";
                break;

            case "Spanish":
                shop_title_text.text = "TIENDA";
                break;

            case "Korean":
                shop_title_text.text = "상점";
                break;
        }
    }

    // 골드를 구매할 수 있는지 표시해주는 함수
    void Buy_gold(string product_type)
    {
        Audio_manager.instance.Play_touch_sound();
        m_cash_value = m_gold_list[product_type];

        if (Data_controller.instance.cash > m_cash_value)
        {
            Shop_alert_manager.instance.Run_thanks_for_buying_alert();
            Data_controller.instance.cash -= m_cash_value;

            switch (product_type) // 아이템 종류별로 구매
            {
                case "small_amount_of_gold":
                    Data_controller.instance.gold += 250000;
                    break;

                case "medium_amount_of_gold":
                    Data_controller.instance.gold += 600000;
                    break;

                case "bag_of_gold":
                    Data_controller.instance.gold += 1300000;
                    break;

                case "chest_of_gold":
                    Data_controller.instance.gold += 2800000;
                    break;
            }
        }
        else 
            Shop_alert_manager.instance.Run_not_enough_money_alert();
    }

    // 부스터를 구매할 수 있는지 표시해주는 함수
    void Buy_booster(string product_type)
    {
        Audio_manager.instance.Play_touch_sound();
        m_cash_value = m_item_list[product_type];

        if (Data_controller.instance.cash > m_cash_value)
        {
            Shop_alert_manager.instance.Run_thanks_for_buying_alert();
            Data_controller.instance.cash -= m_cash_value;

            switch (product_type) // 아이템 종류별로 구매
            {
                case "booster_fifteen_minutes":
                    Time_manager.instance.Set_gameplay_booster_values(false, 900f);
                    break;

                case "booster_thirty_minutes":
                    Time_manager.instance.Set_gameplay_booster_values(false, 1800f);
                    break;
            }
        }
        else 
            Shop_alert_manager.instance.Run_not_enough_money_alert();
    }

    // 게임의 캐시를 추가해주는 함수
    public void addCash(int cashBought)
    {
        Audio_manager.instance.Play_touch_sound();
        Shop_alert_manager.instance.Run_thanks_for_buying_alert();
        Data_controller.instance.cash += cashBought;
    }
}