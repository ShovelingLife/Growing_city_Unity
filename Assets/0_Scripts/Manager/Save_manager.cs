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
    Player_data                 player_data;
    public In_app_manager       in_app_manager;
    Crop_menu_data[]            arr_crop_menu_data;
    City_menu_data[]            arr_city_menu_data;
    Automation_menu_data[]      arr_automation_menu_data;
    Resident_menu_data[]        arr_resident_menu_data;
    Building_menu_data[]        arr_building_menu_data;
    Building_upgrade[]          arr_tmp_building_upgrade_button;
    int[]                       arr_upgrade_menu_index;
    Upgrade_menu_manager        m_inst;


    private void Awake()
    {
        m_inst = Upgrade_menu_manager.instance;
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
        arr_tmp_building_upgrade_button = Resources.FindObjectsOfTypeAll<Building_upgrade>();
        arr_tmp_building_upgrade_button.OrderBy(x => x.name);
    }

    // 게임 오브젝트 배열 초기화
    void Init_menu_objects_array()
    {

        // 데이터 배열 크기 설정 0번은 제외
        arr_upgrade_menu_index = new int[4];
        arr_upgrade_menu_index[0] = m_inst.upgrade_button_storage.total_crop_upgrade;
        arr_upgrade_menu_index[1] = arr_upgrade_menu_index[0] + m_inst.upgrade_button_storage.total_city_upgrade; ;
        arr_upgrade_menu_index[2] = arr_upgrade_menu_index[1] + m_inst.upgrade_button_storage.total_resident_upgrade; ;
        //arr_total_upgrade_button[0] = arr_crop_upgrade_button.Length;

        arr_crop_menu_data       = new Crop_menu_data[m_inst.upgrade_button_storage.total_crop_upgrade];
        arr_city_menu_data       = new City_menu_data[m_inst.upgrade_button_storage.total_city_upgrade];
        arr_resident_menu_data   = new Resident_menu_data[m_inst.upgrade_button_storage.total_resident_upgrade];
        arr_automation_menu_data = new Automation_menu_data[m_inst.upgrade_button_storage.total_automation_upgrade];
        //arr_building_menu_data = new Crop_menu_data[arr_crop_upgrade_button.Length];
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
        for (int i = 0; i < arr_crop_menu_data.Length; i++)
        {
            arr_crop_menu_data[i] = new Crop_menu_data()
            {
                crop_level         = m_inst.upgrade_button_storage.arr_crop_upgrade_button[i].current_level,
                crop_upgrade_price = m_inst.upgrade_button_storage.arr_crop_upgrade_button[i].upgrade_price,
                crop_gold_per_sec  = Data_controller.instance.gold_per_click_gold
            };
        }
        string json = Json_helper.ToJson(arr_crop_menu_data, true);
        File.WriteAllText(Application.persistentDataPath + "/crop_menu_data.json", json);
    }

    // City 메뉴 저장
    void Save_city_menu_data()
    {
        for (int i = 0; i < arr_city_menu_data.Length; i++)
        {
            arr_city_menu_data[i] = new City_menu_data()
            {
                city_level         = m_inst.upgrade_button_storage.arr_city_upgrade_button[i].current_level,
                city_upgrade_price = m_inst.upgrade_button_storage.arr_city_upgrade_button[i].upgrade_price,
                city_gold_per_sec  = Data_controller.instance.gold_per_click_city
            };
        }
        string json = Json_helper.ToJson(arr_city_menu_data, true);
        File.WriteAllText(Application.persistentDataPath + "/city_menu_data.json", json);
    }

    // Resident 메뉴 저장
    void Save_resident_menu_data()
    {
        for (int i = 0; i < arr_resident_menu_data.Length; i++)
        {
            arr_resident_menu_data[i] = new Resident_menu_data()
            {
                resident_level         = m_inst.upgrade_button_storage.arr_resident_upgrade_button[i].current_level,
                resident_upgrade_price = m_inst.upgrade_button_storage.arr_resident_upgrade_button[i].upgrade_price,
                resident_gold_per_sec  = Data_controller.instance.gold_per_sec_resident
            };
        }
        string json = Json_helper.ToJson(arr_resident_menu_data, true);
        File.WriteAllText(Application.persistentDataPath + "/resident_menu_data.json", json);
    }

    // Automation 메뉴 저장
    void Save_automation_menu_data()
    {
        for (int i = 0; i < arr_automation_menu_data.Length; i++)
        {
            arr_automation_menu_data[i] = new Automation_menu_data()
            {
                automation_level         = m_inst.upgrade_button_storage.arr_automation_upgrade_button[i].current_level,
                automation_upgrade_price = m_inst.upgrade_button_storage.arr_automation_upgrade_button[i].upgrade_price,
                automation_gold_per_sec  = Data_controller.instance.gold_per_sec_automation
            };
        }
        string json = Json_helper.ToJson(arr_automation_menu_data, true);
        File.WriteAllText(Application.persistentDataPath + "/automation_menu_data.json", json);
    }

    // 건물 메뉴 저장
    void Save_building_menu_data()
    {
        int i = 0;

        foreach (var item in arr_tmp_building_upgrade_button)
        {
            arr_building_menu_data[i++] = new Building_menu_data()
            {
                building_gold_per_sec  = Data_controller.instance.gold_per_sec_building,
                building_level         = item.current_level,
                building_upgrade_price = item.upgrade_price
            };
        }
        string json = Json_helper.ToJson(arr_building_menu_data, true);
        File.WriteAllText(Application.persistentDataPath + "/building_menu_data.json", json);
    }

    // 플레이어 저장 데이터 불러오기
    void Load_player_data()
    {
        string save_string = File.ReadAllText(Application.persistentDataPath + "/player_data.json");
        player_data        = JsonUtility.FromJson<Player_data>(save_string);

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
        arr_crop_menu_data = Json_helper.FromJson<Crop_menu_data>(save_string);

        // 파일이 있을 시 json
        if (File.Exists(Application.persistentDataPath + "/crop_menu_data.json"))
        {
            for (int i = 0; i < arr_crop_menu_data.Length; i++)
            {
                m_inst.upgrade_button_storage.arr_crop_upgrade_button[i].current_level     = arr_crop_menu_data[i].crop_level;
                m_inst.upgrade_button_storage.arr_crop_upgrade_button[i].upgrade_price     = arr_crop_menu_data[i].crop_upgrade_price;
                Data_controller.instance.gold_per_click_gold = arr_crop_menu_data[i].crop_gold_per_sec;
            }
        }
        // 파일이 없을 시 csv
        else
        {
            for (int i = 0; i < arr_crop_menu_data.Length; i++)
            {
                m_inst.upgrade_button_storage.arr_crop_upgrade_button[i].current_level = 1;
                m_inst.upgrade_button_storage.arr_crop_upgrade_button[i].upgrade_price = Csv_loader_manager.instance.Get_price_from_dic("UPGRADE_PRICE",i);
                m_inst.upgrade_button_storage.arr_crop_upgrade_button[i].cost_pow      = (float)Csv_loader_manager.instance.Get_price_from_dic("PRICE_INCREMENT", i);
            }
        }
    }

    // City 메뉴 불러오기
    void Load_city_menu_data()
    {
        string save_string = File.ReadAllText(Application.persistentDataPath + "/city_menu_data.json");
        arr_city_menu_data = Json_helper.FromJson<City_menu_data>(save_string);

        // 파일이 있을 시 json
        if (File.Exists(Application.persistentDataPath + "/city_menu_data.json"))
        {
            for (int i = 0; i < arr_city_menu_data.Length; i++)
            {
                m_inst.upgrade_button_storage.arr_city_upgrade_button[i].current_level         = arr_city_menu_data[i].city_level;
                m_inst.upgrade_button_storage.arr_city_upgrade_button[i].upgrade_price         = arr_city_menu_data[i].city_upgrade_price;
                Data_controller.instance.gold_per_click_city = arr_city_menu_data[i].city_gold_per_sec;
            }
        }
        // 파일이 없을 시 csv
        else
        {
            for (int i = 0; i < arr_city_menu_data.Length; i++)
            {
                m_inst.upgrade_button_storage.arr_city_upgrade_button[i].current_level = 1;
                m_inst.upgrade_button_storage.arr_city_upgrade_button[i].upgrade_price = Csv_loader_manager.instance.Get_price_from_dic("UPGRADE_PRICE", arr_upgrade_menu_index[0] + i);
                m_inst.upgrade_button_storage.arr_city_upgrade_button[i].cost_pow      = (float)Csv_loader_manager.instance.Get_price_from_dic("PRICE_INCREMENT", arr_upgrade_menu_index[0] + i);
            }
        }
    }

    // Resident 메뉴 불러오기
    void Load_resident_menu_data()
    {
        string save_string     = File.ReadAllText(Application.persistentDataPath + "/resident_menu_data.json");
        arr_resident_menu_data = Json_helper.FromJson<Resident_menu_data>(save_string);

        // 파일이 있을 시 json
        if (File.Exists(Application.persistentDataPath + "/resident_menu_data.json"))
        {
            for (int i = 0; i < arr_resident_menu_data.Length; i++)
            {
                m_inst.upgrade_button_storage.arr_resident_upgrade_button[i].current_level = arr_resident_menu_data[i].resident_level;
                m_inst.upgrade_button_storage.arr_resident_upgrade_button[i].upgrade_price = arr_resident_menu_data[i].resident_upgrade_price;
                Data_controller.instance.gold_per_sec_resident  = arr_resident_menu_data[i].resident_gold_per_sec;
            }
        }
        // 파일이 없을 시 csv
        else
        {
            for (int i = 0; i < arr_crop_menu_data.Length; i++)
            {
                m_inst.upgrade_button_storage.arr_resident_upgrade_button[i].current_level = 1;
                m_inst.upgrade_button_storage.arr_resident_upgrade_button[i].upgrade_price = Csv_loader_manager.instance.Get_price_from_dic("UPGRADE_PRICE", arr_upgrade_menu_index[1] + i);
                m_inst.upgrade_button_storage.arr_resident_upgrade_button[i].cost_pow      = (float)Csv_loader_manager.instance.Get_price_from_dic("PRICE_INCREMENT", arr_upgrade_menu_index[1] + i);
            }
        }
    }

    // Automation 메뉴 불러오기
    void Load_automation_menu_data()
    {
        string save_string       = File.ReadAllText(Application.persistentDataPath + "/automation_menu_data.json");
        arr_automation_menu_data = Json_helper.FromJson<Automation_menu_data>(save_string);
        
        // 파일이 있을 시 json
        if (File.Exists(Application.persistentDataPath + "/automation_menu_data.json"))
        {
            for (int i = 0; i < arr_automation_menu_data.Length; i++)
            {
                m_inst.upgrade_button_storage.arr_automation_upgrade_button[i].current_level = arr_automation_menu_data[i].automation_level;
                m_inst.upgrade_button_storage.arr_automation_upgrade_button[i].upgrade_price = arr_automation_menu_data[i].automation_upgrade_price;
                Data_controller.instance.gold_per_sec_automation  = arr_automation_menu_data[i].automation_gold_per_sec;
            }
        }
        // 파일이 없을 시 csv
        else
        {
            for (int i = 0; i < arr_crop_menu_data.Length; i++)
            {
                m_inst.upgrade_button_storage.arr_automation_upgrade_button[i].current_level = 1;
                m_inst.upgrade_button_storage.arr_automation_upgrade_button[i].upgrade_price = Csv_loader_manager.instance.Get_price_from_dic("UPGRADE_PRICE", arr_upgrade_menu_index[2] + i);
                m_inst.upgrade_button_storage.arr_automation_upgrade_button[i].cost_pow      = (float)Csv_loader_manager.instance.Get_price_from_dic("PRICE_INCREMENT", arr_upgrade_menu_index[2] + i);
            }
        }
    }

    // 건물 메뉴 불러오기
    void Load_building_menu_data()
    {
        string save_string     = File.ReadAllText(Application.persistentDataPath + "/building_menu_data.json");
        arr_building_menu_data = Json_helper.FromJson<Building_menu_data>(save_string);

        if (File.Exists(Application.persistentDataPath + "/building_menu_data.json"))
        {
            int i = 0;
            foreach (var item in arr_tmp_building_upgrade_button)
            {
                item.current_level = arr_building_menu_data[i].building_level;
                item.upgrade_price = arr_building_menu_data[i].building_upgrade_price;
                i++;
            }
            //Data_controller.instance.gold_per_sec_building = arr_building_menu_data[0].building_gold_per_sec;
        }
        else return;
    }
}