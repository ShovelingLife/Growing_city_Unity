using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ------- HASH CODE -------

// 0   AUTOMATION_BUTTON
// 1   BOOSTER_TEXT	
// 2   BOOSTER_TIME	
// 3   CITY_BUTTON	
// 4   CLAIM	
// 5   CROP_BUTTON	/ CropText
// 6   DAILY_REWARD	
// 7   DAILY_REWARD_AVAILABLE	
// 8   DIAMOND	
// 9   GOLD	
// 10  INSUFFICIENT_MONEY	/ NotEnoughCashText
// 11  INTRO_BUTTON	
// 12  LEVEL	
// 13  MAX_LEVEL	
// 14  MAX_MONEY	
// 15  NO_ADS	
// 16  PER_SECOND	
// 17  RESIDENT_BUTTON	
// 18  REWARD	
// 19  SHOP_TITLE	
// 20  THANKS_FOR_PURCHASE	/ ThanksForBuyingText
// 21  UPGRADE	
// 22  WELCOME_BACK	
// 23  EXIT
// 24  YES	
// 25  NO	

public class Csv_loader_manager : Singleton_local<Csv_loader_manager>
{
    // ------- 변수 목록 -------
    List<Dictionary<string, object>> m_list_language_csv = new List<Dictionary<string, object>>();
    List<Dictionary<string, object>> m_list_upgrade_csv  = new List<Dictionary<string, object>>();
    Dictionary<string, int>          m_dic_hash_table    = new Dictionary<string, int>();
    string                           m_current_language  = "english";

    // 현재 언어를 가져옴
    public string current_language_prop{ 
        get { return m_current_language; }
        set { m_current_language = value; }
    }

    // 해쉬코드 통해 접근
    public object this[int _code]
    {
        get { return m_list_language_csv[_code][m_current_language.ToUpper()]; }
    }

    void Awake()
    {
        Init_hash_table();
    }

    // 해쉬테이블 코드 초기화
    void Init_hash_table()
    {
        m_list_language_csv = Csv_reader.Read("Csv_files/Growing_city_translation");
        m_list_upgrade_csv = Csv_reader.Read("Csv_files/Growing_city_upgrade_price");
        int i = 0;

        foreach (var item in m_list_language_csv)
                 m_dic_hash_table.Add(item["MENU_TYPE"].ToString(), i++);
    }

    // 현재 언어 설정
    public void Set_language(string _language)
    {
        m_current_language = _language;
    }

    // 문자열로 해쉬 코드 가져옴
    public int Get_hash_code_by_str(string _key)
    {
        return m_dic_hash_table[_key];
    }

    // 업그레이드 값 가져오기
    public double Get_price_from_dic(string _key, int _index)
    {
        return Convert.ToDouble(m_list_upgrade_csv[_index][_key]);
    }
}
