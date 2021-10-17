using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 상위 UI를 담당하는 클래스 (곱해주는 버튼, 전체 획득량)
public class Upgrade_property_core : MonoBehaviour
{
    // 각 업그레이드 항목 스크롤
    public Dictionary<e_upgrade_menu_type, Upgrade_property_core> dic_current_upgrades = new Dictionary<e_upgrade_menu_type, Upgrade_property_core>();

    // 곱해주는 버튼
    [SerializeField]
    protected Button[]           m_arr_multiplier_button = new Button[3];

    // 초당 텍스트
    public    Text               txt_per_sec;
    public    int                upgrade_menu_code;
    protected Csv_loader_manager m_csv_inst;


    // Start is called before the first frame update
    protected virtual void Awake()
    {
        m_csv_inst = Csv_loader_manager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_csv_inst)
           txt_per_sec.text = m_csv_inst[m_csv_inst.Get_hash_code_by_str("PER_SECOND")] + " " + Large_number.ToString(Data_controller.instance.arr_all_per_sec[upgrade_menu_code]);
    }

    // 버튼 초기화
    protected void Init_buttons(e_upgrade_menu_type _upgrade_menu_type)
    {
        for (int i = 0; i < m_arr_multiplier_button.Length; i++)
             m_arr_multiplier_button[i] = transform.GetChild(0).transform.GetChild(i).GetComponent<Button>();

        // 버튼들을 메뉴에 맞게 초기화
        switch (_upgrade_menu_type)
        {
            case e_upgrade_menu_type.CROP:
                m_arr_multiplier_button[0].onClick.AddListener(Upgrade_menu_manager.instance.Apply_multiplier1_on_crop_upgrade_menu);
                m_arr_multiplier_button[1].onClick.AddListener(Upgrade_menu_manager.instance.Apply_multiplier10_on_crop_upgrade_menu);
                m_arr_multiplier_button[2].onClick.AddListener(Upgrade_menu_manager.instance.Apply_multiplier50_on_crop_upgrade_menu);
                break;

            case e_upgrade_menu_type.CITY:
                m_arr_multiplier_button[0].onClick.AddListener(Upgrade_menu_manager.instance.Apply_multiplier1_on_city_upgrade_menu);
                m_arr_multiplier_button[1].onClick.AddListener(Upgrade_menu_manager.instance.Apply_multiplier10_on_city_upgrade_menu);
                m_arr_multiplier_button[2].onClick.AddListener(Upgrade_menu_manager.instance.Apply_multiplier50_on_city_upgrade_menu);
                break;

            case e_upgrade_menu_type.RESIDENT:
                m_arr_multiplier_button[0].onClick.AddListener(Upgrade_menu_manager.instance.Apply_multiplier1_on_resident_upgrade_menu);
                m_arr_multiplier_button[1].onClick.AddListener(Upgrade_menu_manager.instance.Apply_multiplier10_on_resident_upgrade_menu);
                m_arr_multiplier_button[2].onClick.AddListener(Upgrade_menu_manager.instance.Apply_multiplier50_on_resident_upgrade_menu);
                break;
            case e_upgrade_menu_type.AUTOMATION:
                m_arr_multiplier_button[0].onClick.AddListener(Upgrade_menu_manager.instance.Apply_multiplier1_on_automation_upgrade_menu);
                m_arr_multiplier_button[1].onClick.AddListener(Upgrade_menu_manager.instance.Apply_multiplier10_on_automation_upgrade_menu);
                m_arr_multiplier_button[2].onClick.AddListener(Upgrade_menu_manager.instance.Apply_multiplier50_on_automation_upgrade_menu);
                break;
        }

        //SpriteState first_button_state = m_arr_multiplier_button[0].spriteState;
        //m_arr_multiplier_button[0].GetComponent<Image>().sprite = m_arr_multiplier_button[0].spriteState.selectedSprite;
    }    
}
