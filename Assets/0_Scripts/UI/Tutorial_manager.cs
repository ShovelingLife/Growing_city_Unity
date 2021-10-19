using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Tutorial_manager : Singleton_local<Tutorial_manager>
{
    // ------- InGame Buttons -------
    Pause_menu        m_pause_menu;
    public Button     btn_option;
    public Transform  trans_language_btn;
    public Transform  trans_close_option_menu_btn;
    public Transform  trans_second_map_btn;
    public Transform  trans_first_map_btn;
    public Transform  trans_crop_menu_first_upgrade_btn;

    // ------- Another Variables -------
    public Canvas     canvas_mainUI;
    public Canvas     canvas_subUI;
    public GameObject obj_text_panel;
    public Transform  trans_select_image;
    public GameObject crop_menu;
    public Text       txt_tutorial1;
    public Text       txt_tutorial2;
    public Text       txt_tutorial3;
    public Text       txt_tutorial4;
    public static int step;


    private void Awake()
    {
        if (PlayerPrefs.HasKey("FirstTime")) 
            gameObject.SetActive(false);
    }

    private void Start()
    {
        m_pause_menu = GameObject.FindObjectOfType<Pause_menu>();
    }

    void Update()
    {
        Init_tutorial();
        Show_text();
    }

    // 튜토리얼 시작
    void Init_tutorial()
    {
        Vector3 new_pos = new Vector3();
        Button btn_next_map = trans_second_map_btn.gameObject.GetComponent<Button>();

        switch (step)
        {
            case 0:
                btn_option.interactable = true;
                btn_next_map.interactable = false;

                for (int i = 0; i < 4; i++)
                     Upgrade_menu_manager.instance.arr_menu_button[i].interactable = false;

                break;

            case 1:
                this.transform.SetParent(btn_option.transform);
                new_pos                          = trans_language_btn.position;
                new_pos.y                       += 1.5f;
                trans_select_image.position      = new_pos;
                m_pause_menu.arr_menu[0].GetComponent<Button>().interactable = false;
                break;

            case 2:
                this.transform.SetParent(trans_language_btn);
                new_pos                     = trans_close_option_menu_btn.position;
                new_pos.y                  += 1.5f;
                trans_select_image.position = new_pos;
                m_pause_menu.arr_menu[0].GetComponent<Button>().interactable = true;
                break;

            case 3:
                this.transform.SetParent(canvas_mainUI.transform);
                new_pos                     = trans_close_option_menu_btn.position;
                new_pos.y                  += 1.5f;
                trans_select_image.position = new_pos;
                break;

            case 4:
                new_pos                         = trans_second_map_btn.position;
                new_pos.y                      += 1.5f;
                btn_next_map.interactable = true;
                trans_select_image.position     = new_pos;
                break;

            case 5:
                this.transform.SetParent(canvas_subUI.transform);
                new_pos                         = trans_first_map_btn.position;
                new_pos.y                      += 1.5f;
                btn_option.interactable         = false;
                btn_next_map.interactable = false;
                trans_select_image.position     = new_pos;
                break;

            case 6:
                this.transform.SetParent(canvas_mainUI.transform);
                new_pos                     = m_pause_menu.arr_menu[0].transform.position;
                new_pos.y                  += 2.5f;
                Upgrade_menu_manager.instance.arr_menu_button[0].interactable = true;
                trans_select_image.position = new_pos;
                break;

            case 7:
                this.transform.SetParent(trans_crop_menu_first_upgrade_btn.transform);
                new_pos                     = trans_crop_menu_first_upgrade_btn.position;
                new_pos.y                  += 1.5f;
                this.transform.SetParent(crop_menu.transform);
                trans_select_image.position = new_pos;
                break;

            case 8:
                this.transform.SetParent(canvas_mainUI.transform);
                new_pos                         = canvas_mainUI.transform.position;
                new_pos.y                       += 5.25f;
                new_pos.x                       += 0.5f;
                trans_select_image.position = new_pos;
                break;

            case 9:
                StopAllCoroutines();
                PlayerPrefs.SetInt("FirstTime", 1);
                PlayerPrefs.Save();
                btn_option.interactable          = true;
                btn_next_map.interactable        = true;

                for (int i = 0; i < 4; i++)
                     Upgrade_menu_manager.instance.arr_menu_button[i].interactable = true;

                Destroy(this.gameObject);
                break;
        }
    }

    // 메시지 띄움
    void Show_text()
    {
        switch (step)
        {
            case 3:
                obj_text_panel.SetActive(true);
                txt_tutorial1.enabled      = true;
                txt_tutorial2.enabled     = false;
                txt_tutorial3.enabled    = false;
                txt_tutorial4.enabled = false;
                break;

            case 5: // 패널 위치를 sub canvas에서 조정
                Vector2 new_panel_pos                  = canvas_subUI.transform.position;
                new_panel_pos.y                        += 10f;
                obj_text_panel.SetActive(true);
                obj_text_panel.transform.position = new_panel_pos;
                txt_tutorial1.enabled                   = false;
                txt_tutorial2.enabled                  = true;
                break;

            case 6:
                obj_text_panel.SetActive(true);
                txt_tutorial2.enabled                  = false;
                txt_tutorial3.enabled                 = true;
                obj_text_panel.transform.position = canvas_mainUI.transform.position;
                break;

            case 8:
                Vector3 new_pos                        = canvas_mainUI.transform.position;
                new_pos.y                              += 12.5f;
                obj_text_panel.SetActive(true);
                txt_tutorial3.enabled                 = false;
                txt_tutorial4.enabled              = true;
                obj_text_panel.transform.position = new_pos;
                break;

            default:
                obj_text_panel.SetActive(false);
                break;
        }
    }
}