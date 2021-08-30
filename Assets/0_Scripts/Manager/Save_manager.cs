using UnityEngine;
using System.IO;
using System;
using System.Linq;

[System.Serializable]
public class Player_data
{
    public double gold;
    public float boost_value;
    public int cash;
}

[Serializable]
class Crop_menu_data
{
    public int crop_level = 0;
    public double crop_upgrade_price = 0;
    public double crop_gold_per_sec;
}

[Serializable]
class City_menu_data
{
    public int city_level=0;
    public double city_upgrade_price=0f;
    public double city_gold_per_sec=0f;
}

[Serializable]
class Resident_menu_data
{
    public int resident_level = 0;
    public double resident_upgrade_price = 0f;
    public double resident_gold_per_sec = 0f;
}

[Serializable]
class Automation_menu_data
{
    public int automation_level=0;
    public double automation_upgrade_price=0f;
    public double automation_gold_per_sec=0f;
}

[Serializable]
class Building_menu_data
{
    public int building_level = 0;
    public double building_upgrade_price = 0f;
    public double building_gold_per_sec = 0f;
}

[Serializable]
public class Save_manager : Singleton_local<Save_manager>
{
    Player_data player_data;
    public In_app_manager in_app_manager;
    Crop_menu_data []crop_menu_data                        = new Crop_menu_data[6];
    City_menu_data []city_menu_data                        = new City_menu_data[6];
    Automation_menu_data []automation_menu_data            = new Automation_menu_data[6];
    Resident_menu_data []resident_menu_data                = new Resident_menu_data[6];
    Building_menu_data[] building_menu_data                = new Building_menu_data[8];
    Upgrade_button_crop[] tmp_crop_menu_data                 = null;
    Upgrade_button_city[] tmp_city_menu_data_arr             = null;
    Upgrade_button_resident[] tmp_resident_menu_data_arr     = null;
    Upgrade_button_automation[] tmp_automation_menu_data_arr = null;
    Building_upgrade[] tmp_building_upgrade_data_arr;

    private void Awake()
    {
        Init_menu_objects_array();
        Init_building_menu_array();
        Load_data_from_file();
    }
    private void OnApplicationQuit()
    {
        Save_data_on_file();
    }

    // 건물 업그레이드 항목 배열 초기화
    void Init_building_menu_array()
    {
        tmp_building_upgrade_data_arr = Resources.FindObjectsOfTypeAll<Building_upgrade>();
        tmp_building_upgrade_data_arr.OrderBy(x => x.name);
    }

    // 게임 오브젝트 배열 초기화
    void Init_menu_objects_array()
    {
        tmp_crop_menu_data = Resources.FindObjectsOfTypeAll<Upgrade_button_crop>();
        tmp_city_menu_data_arr = Resources.FindObjectsOfTypeAll<Upgrade_button_city>();
        tmp_resident_menu_data_arr = Resources.FindObjectsOfTypeAll<Upgrade_button_resident>();
        tmp_automation_menu_data_arr = Resources.FindObjectsOfTypeAll<Upgrade_button_automation>();
        tmp_crop_menu_data.OrderBy(x => x.name);
        tmp_city_menu_data_arr.OrderBy(x => x.name);
        tmp_resident_menu_data_arr.OrderBy(x => x.name);
        tmp_automation_menu_data_arr.OrderBy(x => x.name);
    }

    [ContextMenu("Do Save_data_on_file")]
    // JSON 파일에 저장
    public void Save_data_on_file()
    {
        Save_player_data();
        Save_crop_menu_data();
        Save_city_menu_data();
        Save_resident_menu_data();
        Save_automation_menu_data();
        Save_building_menu_data();
    }

    [ContextMenu("Load_data_from_file")]
    // JSON 파일 불러오기
    public void Load_data_from_file()
    {
        Load_player_data();
        Load_crop_menu_data();
        Load_city_menu_data();
        Load_resident_menu_data();
        Load_automation_menu_data();
        Load_building_menu_data();
    }

    // 플레이어 데이터 저장
    void Save_player_data()
    {
        player_data = new Player_data()
        {
            gold        = Data_controller.instance.gold,
            cash        = Data_controller.instance.cash,
            boost_value = Time_manager.instance.current_boostValue_gameplay
        };
        string json = JsonUtility.ToJson(player_data,true);
        File.WriteAllText(Application.persistentDataPath + "/player_data.json", json);
    }

    // Crop 메뉴 저장
    void Save_crop_menu_data()
    {
        for (int i = 0; i < crop_menu_data.Length; i++)
        {
            crop_menu_data[i] = new Crop_menu_data()
            {
                crop_level         = tmp_crop_menu_data[i].current_level,
                crop_upgrade_price = tmp_crop_menu_data[i].upgrade_price,
                crop_gold_per_sec  = Data_controller.instance.gold_per_click_gold
            };
        }
        string json = Json_helper.ToJson(crop_menu_data, true);
        File.WriteAllText(Application.persistentDataPath + "/crop_menu_data.json", json);
    }

    // City 메뉴 저장
    void Save_city_menu_data()
    {
        for (int i = 0; i < city_menu_data.Length; i++)
        {
            city_menu_data[i] = new City_menu_data()
            {
                city_level         = tmp_city_menu_data_arr[i].current_level,
                city_upgrade_price = tmp_city_menu_data_arr[i].upgrade_price,
                city_gold_per_sec  = Data_controller.instance.gold_per_click_resident
            };
        }
        string json = Json_helper.ToJson(city_menu_data, true);
        File.WriteAllText(Application.persistentDataPath + "/city_menu_data.json", json);
    }

    // Resident 메뉴 저장
    void Save_resident_menu_data()
    {
        for (int i = 0; i < resident_menu_data.Length; i++)
        {
            resident_menu_data[i] = new Resident_menu_data()
            {
                resident_level         = tmp_resident_menu_data_arr[i].current_level,
                resident_upgrade_price = tmp_resident_menu_data_arr[i].upgrade_price,
                resident_gold_per_sec  = Data_controller.instance.gold_per_sec_resident
            };
        }
        string json = Json_helper.ToJson(resident_menu_data, true);
        File.WriteAllText(Application.persistentDataPath + "/resident_menu_data.json", json);
    }

    // Automation 메뉴 저장
    void Save_automation_menu_data()
    {
        for (int i = 0; i < automation_menu_data.Length; i++)
        {
            automation_menu_data[i] = new Automation_menu_data()
            {
                automation_level         = tmp_automation_menu_data_arr[i].current_level,
                automation_upgrade_price = tmp_automation_menu_data_arr[i].upgrade_price,
                automation_gold_per_sec  = Data_controller.instance.gold_per_sec_automation
            };
        }
        string json = Json_helper.ToJson(automation_menu_data, true);
        File.WriteAllText(Application.persistentDataPath + "/automation_menu_data.json", json);
    }

    // 건물 메뉴 저장
    void Save_building_menu_data()
    {
        int i = 0;

        foreach (var item in tmp_building_upgrade_data_arr)
        {
            building_menu_data[i] = new Building_menu_data()
            {
                building_gold_per_sec  = Data_controller.instance.gold_per_sec_building,
                building_level         = item.current_level,
                building_upgrade_price = item.upgrade_price
            };
            i++;
        }
        string json = Json_helper.ToJson(building_menu_data, true);
        File.WriteAllText(Application.persistentDataPath + "/building_menu_data.json", json);
    }

    // 플레이어 저장 데이터 불러오기
    public void Load_player_data()
    {
        string save_string = File.ReadAllText(Application.persistentDataPath + "/player_data.json");
        player_data = JsonUtility.FromJson<Player_data>(save_string);

        if (File.Exists(Application.persistentDataPath + "/player_data.json"))
        {
            Data_controller.instance.gold                     = player_data.gold;
            Data_controller.instance.cash                     = player_data.cash;
            Time_manager.instance.current_boostValue_gameplay = player_data.boost_value;
        }
    }

    // Crop 메뉴 불러오기
    void Load_crop_menu_data()
    {
        string save_string = File.ReadAllText(Application.persistentDataPath + "/crop_menu_data.json");
        crop_menu_data = Json_helper.FromJson<Crop_menu_data>(save_string);

        if (File.Exists(Application.persistentDataPath + "/crop_menu_data.json"))
        {
            for (int i = 0; i < crop_menu_data.Length; i++)
            {
                tmp_crop_menu_data[i].current_level          = crop_menu_data[i].crop_level;
                tmp_crop_menu_data[i].upgrade_price          = crop_menu_data[i].crop_upgrade_price;
                Data_controller.instance.gold_per_click_gold = crop_menu_data[i].crop_gold_per_sec;
            }
        }
    }

    // City 메뉴 불러오기
    void Load_city_menu_data()
    {
        string save_string = File.ReadAllText(Application.persistentDataPath + "/city_menu_data.json");
        city_menu_data = Json_helper.FromJson<City_menu_data>(save_string);

        if (File.Exists(Application.persistentDataPath + "/city_menu_data.json"))
        {
            for (int i = 0; i < city_menu_data.Length; i++)
            {
                tmp_city_menu_data_arr[i].current_level          = city_menu_data[i].city_level;
                tmp_city_menu_data_arr[i].upgrade_price          = city_menu_data[i].city_upgrade_price;
                Data_controller.instance.gold_per_click_resident = city_menu_data[i].city_gold_per_sec;
            }
        }
    }

    // Resident 메뉴 불러오기
    void Load_resident_menu_data()
    {
        string save_string = File.ReadAllText(Application.persistentDataPath + "/resident_menu_data.json");
        resident_menu_data = Json_helper.FromJson<Resident_menu_data>(save_string);

        if (File.Exists(Application.persistentDataPath + "/resident_menu_data.json"))
        {
            for (int i = 0; i < resident_menu_data.Length; i++)
            {
                tmp_resident_menu_data_arr[i].current_level    = resident_menu_data[i].resident_level;
                tmp_resident_menu_data_arr[i].upgrade_price    = resident_menu_data[i].resident_upgrade_price;
                Data_controller.instance.gold_per_sec_resident = resident_menu_data[i].resident_gold_per_sec;
            }
        }
    }

    // Automation 메뉴 불러오기
    void Load_automation_menu_data()
    {
        string save_string = File.ReadAllText(Application.persistentDataPath + "/automation_menu_data.json");
        automation_menu_data = Json_helper.FromJson<Automation_menu_data>(save_string);

        if (File.Exists(Application.persistentDataPath + "/automation_menu_data.json"))
        {
            for (int i = 0; i < automation_menu_data.Length; i++)
            {
                tmp_automation_menu_data_arr[i].current_level    = automation_menu_data[i].automation_level;
                tmp_automation_menu_data_arr[i].upgrade_price    = automation_menu_data[i].automation_upgrade_price;
                Data_controller.instance.gold_per_sec_automation = automation_menu_data[i].automation_gold_per_sec;
            }
        }
    }

    // 건물 메뉴 불러오기
    public void Load_building_menu_data()
    {
        string save_string = File.ReadAllText(Application.persistentDataPath + "/building_menu_data.json");
        building_menu_data = Json_helper.FromJson<Building_menu_data>(save_string);

        if (File.Exists(Application.persistentDataPath + "/building_menu_data.json"))
        {
            int i = 0;
            foreach (var item in tmp_building_upgrade_data_arr)
            {
                item.current_level = building_menu_data[i].building_level;
                item.upgrade_price = building_menu_data[i].building_upgrade_price;
                i++;
            }
            Data_controller.instance.gold_per_sec_building = building_menu_data[0].building_gold_per_sec;
        }
        else return;
    }
}