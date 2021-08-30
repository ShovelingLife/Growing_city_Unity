using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Tutorial_manager : Singleton_local<Tutorial_manager>
{
    // ------- InGame Buttons -------
    public Button[]   menu_button_arr = new Button[4];
    public Button     exit_option_menu_button;
    public Button     option_button;
    public Button     go_to_next_map_button;
    public GameObject language_button;
    public GameObject close_language_menu_button;
    public GameObject close_option_menu_button;
    public GameObject move_to_next_map_button;
    public GameObject move_to_first_map_button;
    public GameObject crop_menu_button;
    public GameObject crop_menu_first_upgrade_button;

    // ------- Another Variables -------
    public GameObject mainUI_canvas;
    public GameObject subUI_canvas;
    public GameObject tutorial_text_panel;
    public GameObject select_image;
    public GameObject option_menu;
    public GameObject language_menu;
    public GameObject crop_menu;
    public Text       welcome_text;
    public Text       building_text;
    public Text       upgrading_text;
    public Text       touch_screen_text;
    public static int step;


    private void Awake()
    {
        if (PlayerPrefs.HasKey("FirstTime")) 
            gameObject.SetActive(false);
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

        switch (step)
        {
            case 0:
                option_button.interactable = true;
                go_to_next_map_button.interactable = false;

                for (int i = 0; i < 4; i++)
                {
                    menu_button_arr[i].interactable = false;
                }
                break;

            case 1:
                this.transform.SetParent(option_menu.transform);
                new_pos                              = language_button.transform.position;
                new_pos.y                            += 1.5f;
                select_image.transform.position      = new_pos;
                exit_option_menu_button.interactable = false;
                break;

            case 2:
                this.transform.SetParent(language_menu.transform);
                new_pos = close_option_menu_button.transform.position;
                new_pos.y                            += 1.5f;
                select_image.transform.position      = new_pos;
                exit_option_menu_button.interactable = true;
                break;

            case 3:
                this.transform.SetParent(mainUI_canvas.transform);
                new_pos                         = close_option_menu_button.transform.position;
                new_pos.y                       += 1.5f;
                select_image.transform.position = new_pos;
                break;

            case 4:
                new_pos                            = move_to_next_map_button.transform.position;
                new_pos.y                          += 1.5f;
                go_to_next_map_button.interactable = true;
                select_image.transform.position    = new_pos;
                break;

            case 5:
                this.transform.SetParent(subUI_canvas.transform);
                new_pos                            = move_to_first_map_button.transform.position;
                new_pos.y                          += 1.5f;
                option_button.interactable         = false;
                go_to_next_map_button.interactable = false;
                select_image.transform.position    = new_pos;
                break;

            case 6:
                this.transform.SetParent(mainUI_canvas.transform);
                new_pos                         = crop_menu_button.transform.position;
                new_pos.y                       += 2.5f;
                menu_button_arr[0].interactable = true;
                select_image.transform.position = new_pos;
                break;

            case 7:
                this.transform.SetParent(crop_menu_first_upgrade_button.transform);
                new_pos                         = crop_menu_first_upgrade_button.transform.position;
                new_pos.y                       += 1.5f;
                this.transform.SetParent(crop_menu.transform);
                select_image.transform.position = new_pos;
                break;

            case 8:
                this.transform.SetParent(mainUI_canvas.transform);
                new_pos                         = mainUI_canvas.transform.position;
                new_pos.y                       += 5.25f;
                new_pos.x                       += 0.5f;
                select_image.transform.position = new_pos;
                break;

            case 9:
                StopAllCoroutines();
                PlayerPrefs.SetInt("FirstTime", 1);
                PlayerPrefs.Save();
                option_button.interactable          = true;
                go_to_next_map_button.interactable  = true;

                for (int i = 0; i < 4; i++)
                {
                    menu_button_arr[i].interactable = true;
                }
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
                tutorial_text_panel.SetActive(true);
                welcome_text.enabled      = true;
                building_text.enabled     = false;
                upgrading_text.enabled    = false;
                touch_screen_text.enabled = false;
                break;

            case 5: // 패널 위치를 sub canvas에서 조정
                Vector2 new_panel_pos                  = subUI_canvas.transform.position;
                new_panel_pos.y                        += 10f;
                tutorial_text_panel.SetActive(true);
                tutorial_text_panel.transform.position = new_panel_pos;
                welcome_text.enabled                   = false;
                building_text.enabled                  = true;
                break;

            case 6:
                tutorial_text_panel.SetActive(true);
                building_text.enabled                  = false;
                upgrading_text.enabled                 = true;
                tutorial_text_panel.transform.position = mainUI_canvas.transform.position;
                break;

            case 8:
                Vector3 new_pos                        = mainUI_canvas.transform.position;
                new_pos.y                              += 12.5f;
                tutorial_text_panel.SetActive(true);
                upgrading_text.enabled                 = false;
                touch_screen_text.enabled              = true;
                tutorial_text_panel.transform.position = new_pos;
                break;

            default:
                tutorial_text_panel.SetActive(false);
                break;
        }
    }
}