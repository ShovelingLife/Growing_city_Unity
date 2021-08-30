using System.Collections;
using System.Collections.Generic;

// 번역해주는 클래스
public class UI_translation
{
    static string max_money_alert;
    static string translation_perSec;
    static string translation_level;
    static string translation_building_level;
    static string translation_ugrade_text;
    static string translation_crop_menu_title;
    static string translation_city_menu_title;
    static string translation_resident_menu_title;
    static string translation_mechanization_menu_title;
    static string translation_reward_after_time;
    static string translation_daily_shop;
    static string translation_welcome_back;
    static string translation_rewardX2;
    static string translation_item_available;


    // 보유한 돈이 최대치라고 알려주는 함수
    public static string Translate_max_money_alert()
    {
        switch (Lean.Localization.LeanLocalization.CurrentLanguage)
        {
            case "English":
                max_money_alert = "YOU JUST REACHED MAX MONEY.";
                break;

            case "Spanish":
                max_money_alert = "HAS ALCANZADO EL LIMITE DE DINERO.";
                break;

            case "Korean":
                max_money_alert = "보유 가능한 돈의 최대 액수를 초과하셨습니다.";
                break;
        }
        return max_money_alert;
    }

    //  공원 메뉴 제목을 번역해주는 함수
    public static string Translate_crop_menu()
    {
        switch (Lean.Localization.LeanLocalization.CurrentLanguage)
        {
            case "English":
                translation_crop_menu_title = "PARK";
                break;

            case "Spanish":
                translation_crop_menu_title = "PARQUE";
                break;

            case "Korean":
                translation_crop_menu_title = "공원";
                break;
        }
        return translation_crop_menu_title;
    }

    //  도시 메뉴 제목을 번역해주는 함수
    public static string Translate_city_menu()
    {
        switch (Lean.Localization.LeanLocalization.CurrentLanguage)
        {
            case "English":
                translation_city_menu_title = "CITY";
                break;

            case "Spanish":
                translation_city_menu_title = "CIUDAD";
                break;

            case "Korean":
                translation_city_menu_title = "도시";
                break;
        }
        return translation_city_menu_title;
    }

    //  시민 메뉴 제목을 번역해주는 함수
    public static string Translate_resident_menu()
    {
        switch (Lean.Localization.LeanLocalization.CurrentLanguage)
        {
            case "English":
                translation_resident_menu_title = "RESIDENT";
                break;

            case "Spanish":
                translation_resident_menu_title = "RESIDENTE";
                break;

            case "Korean":
                translation_resident_menu_title = "시민";
                break;
        }
        return translation_resident_menu_title;
    }

    //  기계화 메뉴 제목을 번역해주는 함수
    public static string Translate_mechanization_menu()
    {
        switch (Lean.Localization.LeanLocalization.CurrentLanguage)
        {
            case "English":
                translation_mechanization_menu_title = "FACTORY";
                break;

            case "Spanish":
                translation_mechanization_menu_title = "MAQUINA";
                break;

            case "Korean":
                translation_mechanization_menu_title = "기계화";
                break;
        }
        return translation_mechanization_menu_title;
    }

    // 업그레이드 텍스트를 번역해주는 함수
    public static string Translate_upgrade_text()
    {
        switch (Lean.Localization.LeanLocalization.CurrentLanguage)
        {
            case "English":
                translation_ugrade_text = "UPGRADE";
                break;

            case "Spanish":
                translation_ugrade_text = "MEJORAR";
                break;

            case "Korean":
                translation_ugrade_text = "업그레이드";
                break;
        }
        return translation_ugrade_text;
    }

    // 초당 번역해주는 함수
    public static string Translate_perSec_text()
    {
        switch (Lean.Localization.LeanLocalization.CurrentLanguage)
        {
            case "English":
                translation_perSec = "PER SEC";
                break;

            case "Spanish":
                translation_perSec = "POR SEGUNDO";
                break;

            case "Korean":
                translation_perSec = "초당";
                break;
        }
        return translation_perSec;
    }

    // 레벨 번역해주는 함수
    public static string Translate_level_text(int level)
    {
        switch (Lean.Localization.LeanLocalization.CurrentLanguage)
        {
            case "English":
                if(level==250) 
                    translation_level = "MAXED LEVEL";

                else 
                    translation_level = "LEVEL";
                break;

            case "Spanish":
                if (level == 250) 
                    translation_level = "MAXIMO NIVEL";

                else 
                    translation_level = "NIVEL";
                break;

            case "Korean":
                if (level == 250) 
                    translation_level = "최대 단계";

                else 
                    translation_level = "단계";
                break;
        }
        return translation_level;
    }

    // 건물 레벨 번역해주는 함수
    public static string Translate_building_level_text(int level)
    {
        switch (Lean.Localization.LeanLocalization.CurrentLanguage)
        {
            case "English":
                if (level == 100) 
                    translation_building_level = "MAXED LEVEL";

                else 
                    translation_building_level = "LEVEL";
                break;

            case "Spanish":
                if (level == 100) 
                    translation_building_level = "MAXIMO NIVEL";

                else 
                    translation_building_level = "NIVEL";
                break;

            case "Korean":
                if (level == 100) 
                    translation_building_level = "최대 단계";

                else 
                    translation_building_level = "단계";
                break;
        }
        return translation_building_level;
    }

    // 아이템 획득 가능 문구 번역해주는 함수
    public static string Translate_ms_seller_text()
    {
        switch (Lean.Localization.LeanLocalization.CurrentLanguage)
        {
            case "English":
                translation_item_available = "ITEM AVAILABLE";
                break;

            case "Spanish":
                translation_item_available = "OBJECTO DISPONIBLE";
                break;

            case "Korean":
                translation_item_available = "보상 수령 가능";
                break;
        }
        return translation_item_available;
    }

    // 보상 수령하기 번역해주는 함수
    public static string Translate_claim_reward_text()
    {
        switch (Lean.Localization.LeanLocalization.CurrentLanguage)
        {
            case "English":
                translation_reward_after_time = "CLAIM";
                break;

            case "Spanish":
                translation_reward_after_time = "RECLAMAR";
                break;

            case "Korean":
                translation_reward_after_time = "수령하기";
                break;
        }
        return translation_reward_after_time;
    }

    // 일일상점 번역해주는 함수
    public static string Translate_daily_reward_shop_text()
    {
        switch (Lean.Localization.LeanLocalization.CurrentLanguage)
        {
            case "English":
                translation_daily_shop = "DAILY SHOP";
                break;

            case "Spanish":
                translation_daily_shop = "TIENDA DIARIA";
                break;

            case "Korean":
                translation_daily_shop = "일일 상점";
                break;
        }
        return translation_daily_shop;
    }

    // 복귀 메시지 번역해주는 함수
    public static string Translate_welcome_back_text()
    {
        switch (Lean.Localization.LeanLocalization.CurrentLanguage)
        {
            case "English":
                translation_welcome_back = "WELCOME BACK!";
                break;

            case "Spanish":
                translation_welcome_back = "BIENVENIDO DE NUEVO!";
                break;

            case "Korean":
                translation_welcome_back = "돌아와주셔서 감사합니다!";
                break;
        }
        return translation_welcome_back;
    }

    // 복귀 메시지 보상x2 번역해주는 함수
    public static string Translate_welcome_back_rewardX2_text()
    {
        switch (Lean.Localization.LeanLocalization.CurrentLanguage)
        {
            case "English":
                translation_rewardX2 = "REWARD";
                break;

            case "Spanish":
                translation_rewardX2 = "RECOMPENSA";
                break;

            case "Korean":
                translation_rewardX2 = "보상";
                break;
        }
        return translation_rewardX2;
    }
}